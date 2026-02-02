# RabbitMQ preparation

- Showcase `rabbitmq` image and _pgAdmin_
    - Add to `docker-compose.yml`:
    ```yml
    services:
      rabbitmq:
        # management version contains UI
        image: rabbitmq:4-management-alpine
    ```

    - Add to `docker-compose.override.yml`:
    ```yml
    services:
      rabbitmq:
        container_name: rabbitmq
        restart: always
        ports:
          - "5672:5672"     # queue
          - "15672:15672"   # UI
    ```

- Start the service:
  ```sh
  $ docker compose up rabbitmq 
  ```

- Navigate to `localhost:15672`
- Default user/password for UI is `guest`/`guest`


# EventBus.Messages preparation

- Create solution folder: `Webstore/Common
- Add project:
    - Type: Class Library
    - Location: `Webstore/Common/EventBus.Messages`
- Coding:
    - Events
        - `IntegrationBaseEvent.cs`
        - `BasketCheckoutEvent.cs`
    - Common
        - `EventBusConstants.cs`


# Transmitting from Basket.API

- Add reference to `Common/EventBus.Messages`
- Add packages:
  - `MassTransit`
  - `MassTransit.RabbitMQ`
  - `AutoMapper`

- Coding:

  - Entities/
    - `BasketCheckout.cs`
    - `BasketItem.cs`
  - Controllers/
    - `BasketController.cs`
  - Services/
    - `BasketService.cs`
  - Mapper/
    - `BasketProfile.cs`

  - `Program.cs`:
    ```cs
    builder.Services.AddAutoMapper(cfg =>
    {
        cfg.AddMaps(Assembly.GetExecutingAssembly());
    });

    builder.Services.AddMassTransit(config => {
      config.UsingRabbitMq((ctx, cfg) => {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
      });
    });
    ```
  
  - `appsettings.Development.json`:
    ```json
    "EventBusSettings": {
      "HostAddress": "amqp://guest:guest@localhost:5672"
    }
    ```


# Receiving transmissions in Ordering.API

- Add reference to `Common/EventBus.Messages`
- Add packages:
  - `MassTransit`
  - `MassTransit.RabbitMQ`
  - `AutoMapper`

- Coding
  - EventBusConsumers/
    - `BasketCheckoutConsumer.cs`
  - Mapper/
    - `OrderingProfile.cs`

  - `Program.cs`:
    ```cs
    builder.Services.AddAutoMapper(cfg =>
    {
        cfg.AddMaps(Assembly.GetExecutingAssembly());
    });
    
    builder.Services.AddMassTransit(config =>
    {
        config.AddConsumer<BasketCheckoutConsumer>();
        config.UsingRabbitMq((ctx, cfg) =>
        {
            cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
            cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, c =>
            {
                c.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
            });
        });
    });
    ```
  
  - `appsettings.Development.json`:
    ```json
    "EventBusSettings": {
      "HostAddress": "amqp://guest:guest@localhost:5672"
    }
    ```


# Dockerfile updates

- We added a new project reference to `Basket.API` and `Ordering.API`
- So, we need to regenerate `Dockerfile`s

- `docker-compose.override.yml`:
```yml
services:
  basket.api:
    environment:
      - "EventBusSettings:HostAddress=amqp://guest:guest@rabbitmq:5672"
    depends_on:
      - rabbitmq
# ...
  ordering.api:
    environment:
      - "EventBusSettings:HostAddress=amqp://guest:guest@rabbitmq:5672"
    depends_on:
      - rabbitmq
```
