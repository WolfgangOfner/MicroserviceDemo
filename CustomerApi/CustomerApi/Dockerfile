FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CustomerApi/CustomerApi.csproj", "CustomerApi/"]
COPY ["CustomerApi.Data/CustomerApi.Data.csproj", "CustomerApi.Data/"]
COPY ["CustomerApi.Domain/CustomerApi.Domain.csproj", "CustomerApi.Domain/"]
COPY ["CustomerApi.Service/CustomerApi.Service.csproj", "CustomerApi.Service/"]
COPY ["CustomerApi.Messaging.Send/CustomerApi.Messaging.Send.csproj", "CustomerApi.Messaging.Send/"]
COPY ["Tests/CustomerApi.Test/CustomerApi.Test.csproj", "Tests/CustomerApi.Test/"]  
COPY ["Tests/CustomerApi.Service.Test/CustomerApi.Service.Test.csproj", "Tests/CustomerApi.Service.Test/"]  
COPY ["Tests/CustomerApi.Data.Test/CustomerApi.Data.Test.csproj", "Tests/CustomerApi.Data.Test/"] 
COPY ["CustomerApi/nuget.config", ""]
COPY ["*.props", "./"]

ARG PAT=localhost
RUN sed -i "s|</configuration>|<packageSourceCredentials><MicroserviceDemoNugets><add key=\"Username\" value=\"PAT\" /><add key=\"ClearTextPassword\" value=\"${PAT}\" /></MicroserviceDemoNugets></packageSourceCredentials></configuration>|" nuget.config

RUN dotnet restore "CustomerApi/CustomerApi.csproj" --configfile "./nuget.config"
RUN dotnet restore "Tests/CustomerApi.Test/CustomerApi.Test.csproj" --configfile "./nuget.config"
RUN dotnet restore "Tests/CustomerApi.Service.Test/CustomerApi.Service.Test.csproj" --configfile "./nuget.config"
RUN dotnet restore "Tests/CustomerApi.Data.Test/CustomerApi.Data.Test.csproj" --configfile "./nuget.config"
COPY . .

RUN dotnet build "CustomerApi/CustomerApi.csproj" -c Release -o /app/build --no-restore
RUN dotnet build "Tests/CustomerApi.Test/CustomerApi.Test.csproj" -c Release --no-restore
RUN dotnet build "Tests/CustomerApi.Service.Test/CustomerApi.Service.Test.csproj" -c Release --no-restore
RUN dotnet build "Tests/CustomerApi.Data.Test/CustomerApi.Data.Test.csproj" -c Release --no-restore

# database build needs .NET Core 3.1
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS dacpac
ARG BuildId=localhost
LABEL dacpac=${BuildId}
WORKDIR /src
COPY ["CustomerApi.Database.Build/CustomerApi.Database.Build.csproj", "CustomerApi.Database.Build/"]
RUN dotnet restore "CustomerApi.Database.Build/CustomerApi.Database.Build.csproj"
Copy . .
RUN dotnet build "CustomerApi.Database.Build/CustomerApi.Database.Build.csproj" -c Release -o /dacpacs

FROM build AS test  
ARG BuildId=localhost
LABEL test=${BuildId}
RUN dotnet test --no-build -c Release --results-directory /testresults --logger "trx;LogFileName=test_results.trx" /p:CollectCoverage=true /p:CoverletOutputFormat=json%2cCobertura /p:CoverletOutput=/testresults/coverage/ -p:MergeWith=/testresults/coverage/coverage.json  Tests/CustomerApi.Test/CustomerApi.Test.csproj  
RUN dotnet test --no-build -c Release --results-directory /testresults --logger "trx;LogFileName=test_results2.trx" /p:CollectCoverage=true /p:CoverletOutputFormat=json%2cCobertura /p:CoverletOutput=/testresults/coverage/ -p:MergeWith=/testresults/coverage/coverage.json  Tests/CustomerApi.Service.Test/CustomerApi.Service.Test.csproj  
RUN dotnet test --no-build -c Release --results-directory /testresults --logger "trx;LogFileName=test_results3.trx" /p:CollectCoverage=true /p:CoverletOutputFormat=json%2cCobertura /p:CoverletOutput=/testresults/coverage/ -p:MergeWith=/testresults/coverage/coverage.json  Tests/CustomerApi.Data.Test/CustomerApi.Data.Test.csproj

FROM build AS publish
RUN dotnet publish "CustomerApi/CustomerApi.csproj" --no-restore -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerApi.dll"]