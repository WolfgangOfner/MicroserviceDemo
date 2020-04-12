FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Solution/CustomerApi/CustomerApi.csproj", "Solution/CustomerApi/"]
COPY ["Solution/CustomerApi.Domain/CustomerApi.Domain.csproj", "Solution/CustomerApi.Domain/"]
COPY ["Solution/CustomerApi.Messaging.Send/CustomerApi.Messaging.Send.csproj", "Solution/CustomerApi.Messaging.Send/"]
COPY ["Solution/CustomerApi.Service/CustomerApi.Service.csproj", "Solution/CustomerApi.Service/"]
COPY ["Solution/CustomerApi.Data/CustomerApi.Data.csproj", "Solution/CustomerApi.Data/"]
RUN dotnet restore "Solution/CustomerApi/CustomerApi.csproj"
COPY . .
WORKDIR "/src/Solution/CustomerApi"
RUN dotnet build "CustomerApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CustomerApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerApi.dll"]