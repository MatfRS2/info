# Redis

- Redis intro
- Showcase `redis` container
    ```bash
    $ docker run -d --name redis_basket -p 6379:6379 redis
    $ docker exec -it redis_basket /bin/bash
    % redis-cli
    > ping
    > set <KEY> <VALUE>
    > get <KEY>
    ```

# Basket.API init

- Open Rider and load Webstore solution
- Add new solution folder `Services/Basket`
- Create new project
    - name: `Basket.API`
    - path: `Webstore/Services/Basket`
    - Framework: .NET 10
    - Template:  Web.API
    - (opt) Do not use top-level statements
    - Auth:      no auth
    - Advanced:  no https
- Delete premade solution files
- Change runtime APP URL to use port `5001`
- Install:
    - `Swashbuckle.AspNetCore` 
    - `Swashbuckle.AspNetCore.Swagger` 
    - `Swashbuckle.AspNetCore.SwaggerGen` 
    - `Swashbuckle.AspNetCore.SwaggerUI` 
    - NEW: `Newtonsoft.Json` 
    - NEW: `Microsoft.Extensions.Caching.StackExchangeRedis` 
- Add to `Program.cs`:
    ```cs
    ---
      | builder.Services.AddAuthorization();
    + | builder.Services.AddSwaggerGen();
    ---
      | if (app.Environment.IsDevelopment())
      | {
      |     app.MapOpenApi();
    + |     app.UseSwagger();
    + |     app.UseSwaggerUI();
      | }
    ---
    ```
- Run, access `localhost:5001/swagger`
    - (opt) `applicationUrl` to auto-open Swagger in `launchSettings`
- VCS point

# Basket.API coding:

- Coding:
    - `Basket.API/Entities`
        - `ShoppingCartItem.cs`
        - `ShoppingCart.cs`
    - `Basket.API/Repositories`
        - `IBasketRepository.cs`
        - `BasketRepository.cs`
    - `Basket.API/Controllers`
        - `BasketController.cs`
- Add dependency injection (DI) to Program.cs:
    ```cs
      | builder.Services.AddSwaggerGen();
    + | builder.Services.AddScoped<IBasketRepository, BasketRepository>();
    + | builder.Services.AddControllers();
    ---
    + | app.MapControllers();
      | app.Run();
    ```
- Update `appsettings.Development`:
   ```cs
     | {
     |   "Logging": {
     |     "LogLevel": {
     |       "Default": "Information",
     |       "Microsoft.AspNetCore": "Warning"
     |     }
     |   },
   + |   "CacheSettings" : {
   + |     "ConnectionString": "localhost:6379"
   + |   }
     | }
   ``` 
- VSC point

## (Optional) Basket.API testing/debugging
- Open Swagger, add a sample basket
- Go into `redis-cli` in the `redis_basket` container and run:
    ```bash
    $ docker exec -it redis_basket /bin/bash
    % redis-cli
    > type <added-basket-id>
    hash
    > hkeys <added-basket-id>
    1) "absexp"
    2) "data"
    3) "sldexp"
    > hget <added-basket-id> data
    "{...}"
    ```
- Show Rider debugger

# Basket.API containerization
- In Rider, right click on Basket.API -> Add -> Dockerfile
- Explain generated dockerfile
- **Note** Paths might not be correct, fix
- Showcase container build/run

# Docker Compose
- Add `docker-compose.yml`
- Add `docker-compose.override.yml`
- Showcase build/up/down 
    ```bash
    $ docker compose build
    # ERROR "/Catalog.API.csproj": not found
    # treba vratiti promenjene putanje u Dockerfile
    # ponoviti build, trebalo bi da prodje 
    $ docker compose up [--build]
    $ docker compose stop
    $ docker compose down
    $ docker compose -f ... -f ... up
    $ docker compose up -d
    $ docker compose logs -f
    $ curl localhost:8000/api/v1/catalog 
    "{ ... }"
    $ curl localhost:8001/api/v1/basket/foo 
    "{ ... }"
