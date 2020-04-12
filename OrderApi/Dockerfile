#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Solution/OrderApi/OrderApi.csproj", "Solution/OrderApi/"]
COPY ["Solution/OrderApi.Data/OrderApi.Data.csproj", "Solution/OrderApi.Data/"]
COPY ["Solution/OrderApi.Domain/OrderApi.Domain.csproj", "Solution/OrderApi.Domain/"]
COPY ["Solution/OrderApi.Service/OrderApi.Service.csproj", "Solution/OrderApi.Service/"]
COPY ["Solution/OrderApi.Messaging.Receive/OrderApi.Messaging.Receive.csproj", "Solution/OrderApi.Messaging.Receive/"]
RUN dotnet restore "Solution/OrderApi/OrderApi.csproj"
COPY . .
WORKDIR "/src/Solution/OrderApi"
RUN dotnet build "OrderApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrderApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderApi.dll"]