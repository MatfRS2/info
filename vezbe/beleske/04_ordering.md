# Ordering.Domain

- Create solution folder: `Webstore/Services/Ordering`
- Add project:
    - Type: Class Library
    - Location: `Webstore/Services/Ordering/Ordering.Domain`
- Coding:
    - Common
        - `Entity.cs`
        - `ValueObject.cs`
        - `AggregateRoot.cs`
    - Entities
        - `OrderItem.cs`
    - Exceptions
        - `OrderingDomainException.cs`
    - ValueObjects
        - `Address.cs`
    - Aggregates
        - `Order.cs`

# Ordering.Application

- Add project:
    - Type: Class Library
    - Location: `Webstore/Services/Ordering/Ordering.Application`
- Install:
    - `MediatR`
    - `FluentValidation`
    - `FluentValidation.DependencyInjectionExtensions`
    - `Microsoft.Extensions.Logging.Abstractions`
- Add references:
    - `Ordering.Domain`

- Base coding:
    - Contracts
        - Persistence
            - `IAsyncRepository.cs`
            - `IOrderRepository.cs`
        - Infrastructure
            - `IEmailService.cs`
    - Models
        - `Email.cs`
        - `EmailSettings.cs`

- Queries and commands coding:
    - Features/Orders
        - Queries
            - GetListOfOrders
                - `GetListOfOrdersQuery.cs`
        - ViewModels
            - `OrderViewModel.cs`
            - `OrderItemViewModel.cs`
        - Commands
            - DTOs
                - `OrderItemDTO.cs`
            - CreateOrder
                - `CreateOrderCommand.cs`
            - UpdateOrder
                - `UpdateOrderCommand.cs`
            - DeleteOrder
                - `DeleteOrderCommand.cs`
    - Contracts
        - Factories
            - `IOrderFactory.cs`
            - `IorderViewModelFactory.cs`

- Handlers coding:
    - Features/Orders
        - Queries
            - GetListOfOrders
                - `GetListOfOrdersQueryHandler.cs`
        - Commands
            - CreateOrder
                - `CreateOrderCommandHandler.cs`
            - UpdateOrder
                - `UpdateOrderCommandHandler.cs`
            - DeleteOrder
                - `DeleteOrderCommandHandler.cs`

- Validation coding:
    - Features/Orders/Commands
        - CreateOrder
            - `CreateOrderCommandValidator.cs`
        - UpdateOrder
            - `UpdateOrderCommandValidator.cs`
        - DeleteOrder
            - `DeleteOrderCommandValidator.cs`
    - Exceptions
        - `EntityNotFoundException.cs`
        - `ValidationFailedException.cs`

- Behavior coding:
    - Behaviours
        - `ValidationBehavior.cs`
        - `UnhandledExceptionBehavior.cs`

- Service registration coding:
    - `ApplicationServiceRegistration.cs`

# Ordering API 

- Add project:
    - Type: Web API
    - Location: `Webstore/Services/Ordering/Ordering.API`
- Install:
    - `Microsoft.EntityFrameworkCore.Tools`
    - `Polly`
- Add references:
    - `Ordering.Application`
- Coding:
    - Controllers
        - `OrderController.cs`
    - `Program.cs`
- Run, make sure template works OK
- We will come back to it later once we impl infra

# Ordering Infrastructure 

- Prepare SqlServer container
    - `docker-compose.yml`
    ```yml
    services:
        orderdb:
            image: mcr.microsoft.com/mssql/server:2022-latest

    volumes:
        mssql_data:
    ```
    - `docker-compose.override.yml`
    ```yml
    services:
      orderdb:
        container_name: orderdb
        user: root   # NOTE: required on mssql version > 2017
        environment:
          - SA_PASSWORD=MATF12345678rs2
          - ACCEPT_EULA=Y
        restart: always
        ports:
          - "1433:1433"
        volumes:
          - mssql_data:/var/opt/mssql/data
    ```

- Add project:
    - Type: Class library
    - Location: `Webstore/Services/Ordering/Ordering.Infrastructure`
- Install:
    - `Microsoft.EntityFrameworkCore.SqlServer`
    - `MailKit`
    - `MimeKit`
- Add references:
    - `Ordering.Application`
- Coding:
    - Persistence
        - EntityConfigurations
            - `OrderItemEntityTypeConfiguration.cs`
            - `OrderEntityTypeConfiguration.cs`
        - `OrderContext.cs`
        - `OrderContextSeed.cs`
    - Repositories
        - `RepositoryBase.cs`
        - `OrderRepository.cs`
    - Factories
        - `OrderFactory.cs`
        - `OrderViewModelFactory.cs`
    - Mail
        - `EmailService.cs`

# Ordering API 



# Migrations

- Install `dotnet ef`:
    ```sh
    $ dotnet tool install --global dotnet-ef
    ```

- Navigate to `Ordering.Infrastructure`:
    - Install: `Microsoft.EntityFrameworkCore.Design`

- Invoke in Rider:
    - Right click on `Ordering.Infrastructure`
    - `EntityFrameworkCore`
    - Add migration
    - Name: `InitialCreate`
    - all default options
- Should generate `Migrations/...`

- (CLI, optional) Invoke:
    ```sh
    $ cd .../Webstore/Services/Ordering/Ordering.Infrastructure
    $ dotnet ef migrations add InitialCreate
    ```
    
# Ordering.API

- Add references:
    - `Ordering.Infrastructure`
Add:
    - Extensions
        - `WebAppHostBuilderExtensions.cs`
- Update
    - `Program.cs`
    ```cs
    + | builder.Services.AddApplicationServices();
    + | builder.Services.AddInfrastructureServices(builder.Configuration);
    + | builder.MigrateDatabase<OrderContext>((context, services) =>
    + | {
    + |     var logger = services.GetRequiredService<ILogger<OrderContextSeed>>();
    + |     OrderContextSeed.SeedAsync(context, logger).Wait();
    + | });
    ```
    - `appsettings.Development`:
    ```json
    + | "ConnectionStrings": {
    + |     "OrderingConnectionString": "Server=localhost;Database=OrderDb;User Id=sa;Password= MATF12345678rs2;"
    + | },
    + | "EmailSettings": {
    + |   "Mail": "destin.schroeder4@ethereal.email",
    + |   "DisplayName": "Webstore App",
    + |   "Password": "m7y9PEZzayQj7nkYCZ",
    + |   "Host": "smtp.ethereal.email",
    + |   "Port": 587
    + | }
    ```

- Start db:
    ```sh
    $ docker-compose up -d orderdb
    ```

- Run `Ordering.API`
