#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/backend/Order/Service.Order.WebApi/Service.Order.WebApi.csproj", "src/backend/Order/Service.Order.WebApi/"]
RUN dotnet restore "./src/backend/Order/Service.Order.WebApi/Service.Order.WebApi.csproj"
COPY . .
WORKDIR "/src/src/backend/Order/Service.Order.WebApi"
RUN dotnet build "./Service.Order.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Service.Order.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Service.Order.WebApi.dll"]