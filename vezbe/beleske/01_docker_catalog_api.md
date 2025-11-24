# Docker

- Docker intro
- Commands
    ```bash
    $ docker ps     # prints all running containers 
    $ docker ps -a  # prints all containers
    $ docker ps -q  # prints IDs of running containers
    $ docker start ID/name
    $ docker stop  ID/name
    $ docker rm    ID/name
    $ docker run <image>
    $ docker run --name <container_name> <image>
    $ docker run -d --name <container_name> <image>
    $ docker logs -f <container_name> 
    $ docker exec <container_name> <target>
    $ docker exec -it <container_name> /bin/bash
    ```
- Port/Volume bindings
- Showcase MongoDB container
    ```bash
    $ docker run -d --name mongo_catalog -p 27017:27017 mongo
    $ docker exec -it mongo_catalog /bin/bash
    % mongosh
    > show dbs
    > use <db>
    > db.<collection>.find({})
    > db.<collection>.insertOne({name: 'John', surname: 'Doe'})
    > db.<collection>.updateOne({name: 'John'}, { $set: { updated: true } })
    > db.<collection>.deleteOne({name: 'John'})
    ```

# Webstore init

- Open Rider
- New Solution
    - Framework: .NET 10
    - Template:  Web.API
    - (opt) Do not use top-level statements
    - Auth:      no auth
    - Advanced:  no https
- Delete premade solution
- Add new solution folder `Services/Catalog`
- Add new project
    - name: `Catalog.API`
    - path: `Webstore/Services/Catalog`
    - same opts as above
- Explain run configs
    - Config for deleted project might remain, delete it
- Run it, app should be listening on some port
  Assume `5000`, if not, set in `launchSettings.json`
    ```bash
    $ curl localhost:5000
    # no response, but note no error code!
    $ curl localhost:5000/weatherforecast
    # json response, use jq
    ```
- Install:
    - `Swashbuckle.AspNetCore` 
    - `Swashbuckle.AspNetCore.Swagger` 
    - `Swashbuckle.AspNetCore.SwaggerGen` 
    - `Swashbuckle.AspNetCore.SwaggerUI` 
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
- Run, access `localhost:5000/swagger`
    - (opt) `applicationUrl` to auto-open Swagger in `launchSettings`
- Delete aux files:
    - `Catalog.API.http`
    - `WeatherForecast.cs`
- Update `Program.cs`
    - Remove `WeatherForecast` remnants
    - Use top-level statements if not already so
- Rerun, make sure all is working OK (swagger should be empty now!)
- VCS (version control system) point

# Catalog.API coding:

- Prepare NuGet packages:
    - `MongoDB.Driver`
- Coding:
    - `Catalog.API/Entities`
        - `Product.cs`
    - `Catalog.API/Data`
        - `ICatalogContext.cs`
        - `CatalogContext.cs`
        - `CatalogContextSeed.cs`
    - `Catalog.API/Repositories`
        - `IProductRepository.cs`
        - `ProductRepository.cs`
    - `Catalog.API/Controllers`
        - `CatalogController.cs`
- Add dependency injection (DI) to Program.cs:
    ```cs
      | builder.Services.AddSwaggerGen();
    + | builder.Services.AddScoped<ICatalogContext, CatalogContext>();
    + | builder.Services.AddScoped<IProductRepository, ProductRepository>();
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
   + |   "DatabaseSettings" : {
   + |     "ConnectionString": "mongodb://localhost:27017"
   + |   }
     | }
   ``` 
- VSC point
