# Book Store Microservice App

## About
This repository hosts **BookStoreMicroserviceApp**, an ecommerce platform where sellers can list both paper and electronic books, and customers can browse and purchase them. The project is designed with a microservice architecture to ensure scalability, maintainability, and resiliency.

## Architecture and Patterns
- **Microservice Architecture**: The application consists of independent services, ensuring scalability and resilience.
- **Clean Architecture**: Each service follows Clean Architecture principles.
- **Identity Server**: Built using Blazor component architecture.
- **Domain-Driven Design (DDD)**: Rich domain entities with a focus on domain logic.
- **Event-Driven**: Services communicate via events.
- **Result + Error Pattern**: For handling operations with clear success or failure results.
- **REPR Pattern**: Request-Endpoint-Response programming pattern.
- **Outbox & Inbox Patterns**: Ensures reliable message delivery across services.
- **Repository and Unit of Work Patterns**: For data management consistency.
- **Railway Programming**: Streamlines error handling in complex business logic.
- **Decorator Pattern**: Adds functionality dynamically to objects.
- **Factory Method**: Provides flexible object creation mechanisms.
- **Adapter Pattern**: Ensures compatibility between services.
- **IOptions Pattern**: For handling configuration options.
- **Global Exception Pattern**: Centralized exception handling for better error management.

## Technologies and Stack
- **ASP.NET Core 8**
- **EF Core 8**
- **AutoMapper**
- **FluentValidation**
- **MediatR + CQRS**
- **Ardalis**
- **Permission-Based Authorization**
- **OAuth 2.0 + Duende IdentityServer**
- **Redis**
- **MassTransit + RabbitMQ**
- **gRPC**
- **Serilog**
- **Quartz**
- **OpenTelemetry**
- **Azurite** (for local Azure Blob Storage emulation)
- **Stripe.NET** (for payment processing)
- **YARP** (for API Gateway)
- **Polly** (for resilience policies)
- **ASP.NET Core HealthChecks**
- **Swagger** (for API documentation)
- **Microsoft Identity**
- **GraphQL + HotChocolate**
- **Docker & Docker Compose**

## Additional Features
- **Resilience**: Leveraging retry policies, database duplication, and idempotent event handling.
- **Scalability**: Microservices architecture allows horizontal scaling.
- **Maintainability**: Clean code principles, layered architecture, and clear separation of concerns.

## Setup Instructions

### Option 1: Run via Visual Studio
1. **Install Visual Studio 2022 or newer**: [Installation Guide](https://docs.microsoft.com/en-us/visualstudio/install/install-visual-studio)
2. **Clone the repository**:
   ```bash
   git clone https://github.com/GeorgeRitchie/BookStoreMicroserviceApp
   ```
3. **Configure project settings**:
    - Open the solution in Visual Studio.
    - Modify the `appsettings.json` of each project according to your environment:
      - **Service.Identity**: Set mailing credentials.
      - **Service.Payments.WebApi**: Set Stripe API key.
      - **Service.Catalog.WebApi**: Set Azure Blob Storage connection string and container name.
      - **All Projects**: Update SQL connection strings.
4. **Build and run** the solution in Visual Studio.

### Option 2: Run via Docker Compose
1. **Install Docker & Docker Compose**:
   - [Windows](https://docs.docker.com/desktop/install/windows-install/)
   - [Linux](https://docs.docker.com/desktop/install/linux-install/)
   - [MacOS](https://docs.docker.com/desktop/install/mac-install/)
2. **Start Docker**.
3. **Clone the repository**:
   ```bash
   git clone https://github.com/GeorgeRitchie/BookStoreMicroserviceApp
   ```
4. **Configure Docker Compose**:
   - Navigate to the folder containing `docker-compose.yml`.
   - Update environment variables in the `docker-compose.yml` file as needed:
     - **Mailing credentials**.
     - **Stripe API key**.
     - **Azure Blob Storage connection string & container name**.
     - **SQL database connection strings**.
5. **Run Docker Compose**:
   ```bash
   docker compose up -d
   ```

## Recommendations
- **SSL Setup**: To run in a secure environment, set up SSL certificates for HTTPS either locally or within the containers. Alternatively, run in HTTP (note that gRPC will not work over HTTP).
- **Azure Blob Storage**: For local development, you can use [Azurite](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite?tabs=visual-studio%2Cblob-storage#install-azurite). You can also switch to local file storage by modifying the `InfrastructureServiceInstaller.cs` file [here](https://github.com/GeorgeRitchie/BookStoreMicroserviceApp/blob/master/src/backend/Catalog/Service.Catalog.Infrastructure/ServiceInstallers/InfrastructureServiceInstaller.cs).

   Example modification:
   ```csharp
   public void Install(IServiceCollection services, IConfiguration configuration) =>
       services
           .AddSingleton<IFileManager, LocalFileManager>()
           .TryAddTransient<IEventBus, EventBus>();
   ```

**Note**: Using local file storage is not recommended for production.

---

## License
This project is licensed under the GPL-3.0 license - see the [LICENSE](LICENSE.txt) file for details.

## Contributions
Feel free to submit pull requests or open issues to contribute to the project.
