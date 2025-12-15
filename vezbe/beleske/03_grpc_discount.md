# PostgreSQL

- PostgreSQL intro
- Showcase `posgres` image and _pgAdmin_
    - Add to `docker-compose.yml`:
    ```yml
    services:
        
      discountdb:
      image: postgres

      pgadmin:
      image: dpage/pgadmin4

    volumes:
      postgres_data:
      pgadmin_data:
    ```
    
    - Add to `docker-compose.override.yml`:
    ```yml
    services:

      discountdb:
      container_name: discountdb
      environment:
        - POSTGRES_USER=admin
        - POSTGRES_PASSWORD=admin1234
        - POSTGRES_DB=DiscountDb
      restart: always
      ports:
        - "5432:5432"
      volumes:
        # NOTE: in materials this is set to diff dir
        #       and wont work with newer psql versions
        - postgres_data:/var/lib/postgresql/

      pgadmin:
        container_name: pgadmin
        environment:
          - PGADMIN_DEFAULT_EMAIL=razvoj.softvera.matf@gmail.com
          - PGADMIN_DEFAULT_PASSWORD=admin1234
        restart: always
        ports:
          - "5050:80"
        volumes:
          - pgadmin_data:/root/.pgadmin
    ```
- Run the new containers
  ```sh
  $ docker-compose up discountdb pgadmin
  ```
- Navigate to `localhost:5050`
- Add New Server
  - General
    - Name: DiscountServer
  - Connection (creds from **discount db**)
    - Host name/address: discountdb
    - Port: 5432
    - Maintenance database: postgres
    - Username: admin
    - Password: admin1234
  - Save
- Open `DiscountDb`
- Tools > Query tool
  ```sql
  CREATE TABLE Coupon (
    ID SERIAL PRIMARY KEY NOT NULL,
    ProductName VARCHAR(24) NOT NULL,
    Description TEXT,
    Amount INT
  );
  ```
- ```sql
  INSERT INTO Coupon (ProductName, Description, Amount) 
    VALUES ('IPhone X', 'IPhone Discount', 150);
  INSERT INTO Coupon (ProductName, Description, Amount) 
    VALUES ('Huawei Plus', 'Huawei Discount', 110);
  INSERT INTO Coupon (ProductName, Description, Amount) 
    VALUES ('Xiaomi Mi 9', 'Xiaomi Discount', 75);
  INSERT INTO Coupon (ProductName, Description, Amount) 
    VALUES ('Samsung 10', 'Samsung Discount', 100);
  ```:
- Open `coupon` table 
  - Right click > Refresh
  - Right click > View/Edit Data > All rows
  - Verify data is added

# Discount.Common init

- Open Rider and load Webstore solution
- Add new solution folder `Services/Discount`
- Create new project
  - On the left side, select `Class Library` instead of `Web`
  - name: `Discount.Common`
  - path: `Webstore/Services/Discount`
  - Framework: .NET 10
- Install:
  - `Npgsql`
  - `Dapper`
  - `AutoMapper`
  NOTE: deprecated `AutoMapper.Extensions.Microsoft.DependencyInjection`
  - `Microsoft.Extensions.Configuration`
  - `Microsoft.Extensions.Configuration.Binder`
  - `Microsoft.Extensions.DependencyInjection.Abstractions`

# Discount.Common coding:

- Coding:
  - Entities
    - Coupon.cs
  - Data
    - ICouponContext.cs
    - CouponContext.cs
  - DTOs
    - BaseCouponDTO.cs
    - BaseIdentityCouponDTO.cs
    - CouponDTO.cs
    - CreateCouponDTO.cs
    - UpdateCouponDTO.cs
  - Repositories
    - ICouponRepository.cs
    - CouponRepository.cs
  - Extensions
    - DiscountCommonExtensions.cs

# Discount.API init

- Create new `WebAPI` project
  - name: `Discount.API`
  - path: `Webstore/Services/Discount`
  - Framework: .NET 10
  - Template:  Web.API
  - (opt) Do not use top-level statements
  - Auth:      no auth
  - Advanced:  no https
- Reference `Discount.Common`
  - Add > Reference > `Discount.Common`
- Install:
  - `Swashbuckle.AspNetCore` 
  - `Swashbuckle.AspNetCore.Swagger` 
  - `Swashbuckle.AspNetCore.SwaggerGen` 
  - `Swashbuckle.AspNetCore.SwaggerUI` 

# Discount.API coding

- Coding:
  - Controllers
    - CouponController.cs
  - launchSettings.Development.json
  - Program.cs

- Testing:
  - Start `discountdb` via `docker-compose`
  - Run `Discount.API` via `Rider`
  - Open Swagger, search for `Samsung 10` product name

- Add Dockerfile to `Discount.API`
- Add to `docker-compose.yml`
  ```yml
  services:
    discount.api:
      image: discount.api
      build:
        context: .
        dockerfile: Services/Discount/Discount.API/Dockerfile
  ```
- Add to `docker-compose.override.yml`:
  ```yml
  services:
    discount.api:
      container_name: discount.api
      environment:
        - ASPNETCORE_ENVIRONMENT=Development
        - "DatabaseSettings:ConnectionString=Server=discountdb;Port=5432;Database=DiscountDb;User Id=admin;Password=admin1234;"
      depends_on:
        - discountdb
      ports:
        - "8002:8080"
  ```

# Discount.GRPC init

- Create new `gRPC Service` project
  - name: `Discount.GRPC`
  - path: `Webstore/Services/Discount`
  - Framework: .NET 10
  - Template:  gRPC Service
  - (opt) Do not use top-level statements
- Reference `Discount.Common`
  - Add > Reference > `Discount.Common`
- Install:
  - `Google.Protobuf` 
  - `Grpc.Core` 
  - `Grpc.Tools` 
- Go through the default template
  - `greet.proto`
  - `GreeterService`
- Run the default template
  ```sh
  $ curl --http2-prior-knowledge http://localhost:<PORT>
  Communication with gRPC endpoints must be made through a gRPC client...
  ```
- Use BloomRPC to test
  - Server address: localhost:<PORT>
  - Import `greet.proto` file on the left side
  - Open `SayHello`
  - Send request:
  ```json
  {
    "name": "Ivan"
  }
  ```

# Discount.GRPC coding

- Delete template files
  - `greet.proto`
  - `GreeterService`
- Coding:
  - Protos
    - `coupon.proto`
    - Properties > Build action > Protobuf
  - Services
    - CouponService.cs (just blank file)
- Map `CouponService` in `Program.cs` instead
  of the default `GreeterService`
- Rebuild, Rider should generate base
  `CouponProtoService.CouponProtoServiceBase`
  - If it isn't there, try reloading the project
- Explore interface
- Coding:
  - Services
    - CouponService.cs
  - Add db conn string to `launchSettings.json` (copy it from Discount.API)
  - Program.cs
    ```cs
    ---
      | builder.Services.AddGrpc();
    + | builder.Services.AddDiscountCommonServices();
    + | builder.Services.AddAutoMapper(configuration =>
    + | {
    + |   configuration.CreateMap<CouponDTO, GetDiscountResponse>().ReverseMap();
    + |   configuration.CreateMap<CouponDTO, GetRandomDiscountsResponse.Types.Coupon>().ReverseMap();
    + | });
    ---
- Replace proto file in BloomRPC
- Attempt request for `Samsung 10`, should work
- Add to `docker-compose.yml`
  ```yml
  services:
    discount.grpc:
      image: discount.grpc
      build:
        context: .
        dockerfile: Services/Discount/Discount.GRPC/Dockerfile
  ```
- Add to `docker-compose.override.yml`:
  ```yml
  services:
    discount.grpc:
      container_name: discount.grpc
      environment:
        - ASPNETCORE_ENVIRONMENT=Development
        - "DatabaseSettings:ConnectionString=Server=discountdb;Port=5432;Database=DiscountDb;User Id=admin;Password=admin1234;"
      depends_on:
        - discountdb
      ports:
        - "8003:8080"
  ```
