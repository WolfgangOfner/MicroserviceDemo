FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0.102-ca-patch-buster-slim AS build
WORKDIR /src
COPY ["OrderApi/OrderApi.csproj", "OrderApi/"]
COPY ["OrderApi.Data/OrderApi.Data.csproj", "OrderApi.Data/"]
COPY ["OrderApi.Domain/OrderApi.Domain.csproj", "OrderApi.Domain/"]
COPY ["OrderApi.Service/OrderApi.Service.csproj", "OrderApi.Service/"]
COPY ["OrderApi.Messaging.Receive/OrderApi.Messaging.Receive.csproj", "OrderApi.Messaging.Receive/"]
COPY ["Tests/OrderApi.Test/OrderApi.Test.csproj", "Tests/OrderApi.Test/"]  
COPY ["Tests/OrderApi.Service.Test/OrderApi.Service.Test.csproj", "Tests/OrderApi.Service.Test/"]  
COPY ["Tests/OrderApi.Data.Test/OrderApi.Data.Test.csproj", "Tests/OrderApi.Data.Test/"] 
COPY ["OrderApi/nuget.config", ""]
COPY ["*.props", "./"]

ARG PAT=localhost
RUN sed -i "s|</configuration>|<packageSourceCredentials><MicroserviceDemoNugets><add key=\"Username\" value=\"PAT\" /><add key=\"ClearTextPassword\" value=\"${PAT}\" /></MicroserviceDemoNugets></packageSourceCredentials></configuration>|" nuget.config

RUN dotnet restore "OrderApi/OrderApi.csproj" --configfile "./nuget.config"
RUN dotnet restore "Tests/OrderApi.Test/OrderApi.Test.csproj" --configfile "./nuget.config"
RUN dotnet restore "Tests/OrderApi.Service.Test/OrderApi.Service.Test.csproj" --configfile "./nuget.config"
RUN dotnet restore "Tests/OrderApi.Data.Test/OrderApi.Data.Test.csproj" --configfile "./nuget.config"
COPY . .

RUN dotnet build "OrderApi/OrderApi.csproj" -c Release -o /app/build --no-restore
RUN dotnet build "Tests/OrderApi.Test/OrderApi.Test.csproj" -c Release --no-restore
RUN dotnet build "Tests/OrderApi.Service.Test/OrderApi.Service.Test.csproj" -c Release --no-restore
RUN dotnet build "Tests/OrderApi.Data.Test/OrderApi.Data.Test.csproj" -c Release --no-restore

FROM build AS test  
ARG BuildId=localhost
LABEL test=${BuildId} 
RUN dotnet test --no-build -c Release --results-directory /testresults --logger "trx;LogFileName=test_results.trx" /p:CollectCoverage=true /p:CoverletOutputFormat=json%2cCobertura /p:CoverletOutput=/testresults/coverage/ -p:MergeWith=/testresults/coverage/coverage.json Tests/OrderApi.Test/OrderApi.Test.csproj  
RUN dotnet test --no-build -c Release --results-directory /testresults --logger "trx;LogFileName=test_results2.trx" /p:CollectCoverage=true /p:CoverletOutputFormat=json%2cCobertura /p:CoverletOutput=/testresults/coverage/ -p:MergeWith=/testresults/coverage/coverage.json Tests/OrderApi.Service.Test/OrderApi.Service.Test.csproj  
RUN dotnet test --no-build -c Release --results-directory /testresults --logger "trx;LogFileName=test_results3.trx" /p:CollectCoverage=true /p:CoverletOutputFormat=json%2cCobertura /p:CoverletOutput=/testresults/coverage/ -p:MergeWith=/testresults/coverage/coverage.json Tests/OrderApi.Data.Test/OrderApi.Data.Test.csproj

FROM build AS publish
RUN dotnet publish "OrderApi/OrderApi.csproj" --no-restore -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderApi.dll"]