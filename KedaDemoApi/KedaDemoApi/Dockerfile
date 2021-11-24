FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["KedaDemoApi/KedaDemoApi.csproj", "KedaDemoApi/"]
COPY ["KedaDemo.Messaging/KedaDemo.Messaging.csproj", "KedaDemo.Messaging/"]
COPY ["KedaDemoApi.Test/KedaDemoApi.Test.csproj", "KedaDemoApi.Test/"]
COPY ["*.props", "./"]

RUN dotnet restore "KedaDemoApi/KedaDemoApi.csproj"
RUN dotnet restore "KedaDemoApi.Test/KedaDemoApi.Test.csproj"
COPY . .

RUN dotnet build "KedaDemoApi/KedaDemoApi.csproj" -c Release -o /app/build --no-restore
RUN dotnet build "KedaDemoApi.Test/KedaDemoApi.Test.csproj" -c Release --no-restore

FROM build AS test  
ARG BuildId=localhost
LABEL test=${BuildId}
RUN dotnet test --no-build -c Release --results-directory /testresults --logger "trx;LogFileName=test_results.trx" /p:CollectCoverage=true /p:CoverletOutputFormat=json%2cCobertura /p:CoverletOutput=/testresults/coverage/ -p:MergeWith=/testresults/coverage/coverage.json  KedaDemoApi.Test/KedaDemoApi.Test.csproj

FROM build AS publish
RUN dotnet publish "KedaDemoApi/KedaDemoApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KedaDemoApi.dll"]