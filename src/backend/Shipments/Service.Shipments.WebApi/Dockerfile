# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/backend/Shipments/Service.Shipments.WebApi/Service.Shipments.WebApi.csproj", "src/backend/Shipments/Service.Shipments.WebApi/"]
COPY ["src/Common/Authorization/Authorization.csproj", "src/Common/Authorization/"]
COPY ["src/Common/Infrastructure/Infrastructure.csproj", "src/Common/Infrastructure/"]
COPY ["src/Common/Application/Application.csproj", "src/Common/Application/"]
COPY ["src/Common/Domain/Domain.csproj", "src/Common/Domain/"]
COPY ["src/Common/Shared/Shared.csproj", "src/Common/Shared/"]
COPY ["src/backend/Shipments/Service.Shipments.Infrastructure/Service.Shipments.Infrastructure.csproj", "src/backend/Shipments/Service.Shipments.Infrastructure/"]
COPY ["src/backend/Shipments/Service.Shipments.Application/Service.Shipments.Application.csproj", "src/backend/Shipments/Service.Shipments.Application/"]
COPY ["src/backend/Shipments/Service.Shipments.Domain/Service.Shipments.Domain.csproj", "src/backend/Shipments/Service.Shipments.Domain/"]
COPY ["src/backend/Catalog/Service.Catalog.IntegrationEvents/Service.Catalog.IntegrationEvents.csproj", "src/backend/Catalog/Service.Catalog.IntegrationEvents/"]
COPY ["src/backend/Orders/Service.Orders.IntegrationEvents/Service.Orders.IntegrationEvents.csproj", "src/backend/Orders/Service.Orders.IntegrationEvents/"]
COPY ["src/backend/Shipments/Service.Shipments.IntegrationEvents/Service.Shipments.IntegrationEvents.csproj", "src/backend/Shipments/Service.Shipments.IntegrationEvents/"]
COPY ["src/backend/Shipments/Service.Shipments.Endpoints/Service.Shipments.Endpoints.csproj", "src/backend/Shipments/Service.Shipments.Endpoints/"]
COPY ["src/Common/Endpoints/Endpoints.csproj", "src/Common/Endpoints/"]
COPY ["src/backend/Shipments/Service.Shipments.Persistence/Service.Shipments.Persistence.csproj", "src/backend/Shipments/Service.Shipments.Persistence/"]
COPY ["src/Common/Persistence/Persistence.csproj", "src/Common/Persistence/"]
RUN dotnet restore "./src/backend/Shipments/Service.Shipments.WebApi/Service.Shipments.WebApi.csproj"
COPY . .
WORKDIR "/src/src/backend/Shipments/Service.Shipments.WebApi"
RUN dotnet build "./Service.Shipments.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Service.Shipments.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Service.Shipments.WebApi.dll"]