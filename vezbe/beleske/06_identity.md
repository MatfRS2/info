# IdentityServer preparation

- Create solution folder: `Webstore/Security`
- Add project:
  - Name: `IdentityServer
  - Type: Web API
  - Location: `Webstore/Security`
- Install
  - `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
  - `EntityFrameworkCore.Design`
  - `EntityFrameworkCore.SqlServer`
  - `AutoMapper`
  - `AspNetCore.Authentication.JwtBearer`

- Coding
  - Entities/
    - `User.cs`
    - `Roles.cs`
  - Data/
    - `ApplicationContext.cs`
  - EntityTypeConfigurations/
    - `RoleConfiguration.cs`
  - Extensions/
    - `IdentityExtensions.cs`
  - `Program.cs`
  ```cs
    | var app = builder.Build();
    | 
  + | builder.Services.AddControllers();
  + | builder.Services.AddSwaggerGen();
    | 
  + | builder.Services.ConfigurePersistence(builder.Configuration);
  + | builder.Services.ConfigureIdentity();
  + | builder.Services.ConfigureJWT(builder.Configuration);
  + | builder.Services.ConfigureMiscellaneousServices();
    | 
    | // Configure the HTTP request pipeline.
    | if (app.Environment.IsDevelopment())
    | {
    |     app.MapOpenApi();
  + |     app.UseSwagger();
  + |     app.UseSwaggerUI();
    | }
    | 
  + | app.UseAuthentication();
  + | app.UseAuthorization();
    | 
  + | app.MapControllers();
    | 
    | app.Run();
  ```

- Database setup (same as `orderdb`)
  - `docker-compose.yml`
  ```yml
   services:
     identitydb:
      image: mcr.microsoft.com/mssql/server:2022-latest
   volumes:
     mssql_identity_data:
  ```

  - `docker-compose.override.yml`
  ```yml
  services:
    identitydb:
      container_name: identitydb
      environment:
        - MSSQL_SA_PASSWORD=MATF12345678rs2
        - ACCEPT_EULA=Y
      user: root
      restart: always
      ports:
        - "1434:1433"  # NOTE different host port
      volumes:
        - mssql_identity_data:/var/opt/mssql
  ```

- Add connection string to `appsettings.Development.json`
  (NOTE the comma for port!)
```json
  "ConnectionStrings": {
    "IdentityConnectionString": "Server=localhost,1434;Database=IdentityDb;User Id=sa;Password=MATF12345678rs2;TrustServerCertificate=Yes"
  }
```

(OPTIONAL, if running `dotnet-ef` from CLI) 
Coding
  - Data/
    - `DesignTimeApplicationContextFactory.cs`

- Add initial migration
  `IdentityServer` -> EntityFrameworkCore -> Add Migration
    - Name: `CreateIdentityTables`
    - DbContext class: `ApplicationContext`

- Propagate update to db
  ```bash
  $ docker-compose up -d identitydb
  ```
  
  `IdentityServer` -> EntityFrameworkCore -> Update database
    - Select added migration (should be auto selected)
    - DbContextClass: `ApplicationContext`
    - Additional options -> Connection -> select `IdentityConnectionString`

- Connect to db from Rider (similar for VS)
  - Ensure `Database Tools and SQL` plugin is installed and enabled (should be by default)
  - Tools -> Connect to database
  - Use the connection string from `appsettings.Development.json`
  - Connect
  - Open `IdentityDb` -> `dbo` -> `tables` -> `AspNetRoles`
  - Should see our two roles


## User registration

Coding:
  - DTOs
    - `NewUserDto.cs`
    - `UserDetailsDto.cs`
  - Mapper
    - `IdentityProfile.cs`
  - Controllers
    - Base
      - `RegistrationControllerBase.cs`
    - `AuthenticationController.cs`
      - methods: `RegisterBuyer`, `RegisterAdministrator`
  - Extensions
    - `IdentityExtensions.cs`

- Start from Rider, make sure db is up
- Attempt to register **admin** user:
```json
{
  "firstname": "Pera",
  "lastname": "Peric",
  "username": "rs2",
  "password": "test1234",
  "email": "razvoj.softvera.matf@gmail.com",
  "phonenumber": "123-456789"
}
```

- Attempt to register **buyer** user:
```json
{
  "firstname": "Ana",
  "lastname": "Peric",
  "username": "anaperic",
  "password": "test1234",
  "email": "ana.peric@matf.rs",
  "phonenumber": "123-456789"
}
```

- Both should return code `201`

## JWT

- Add to `appsettings.Development.json`
```json
"JwtSettings": {
  "validIssuer": "Webstore Identity",
  "validAudience": "Webstore",
  "secretKey": "MyVerySecretMessage"
}
```

- Coding:
  - Extensions
    - `IdentityExtensions.cs`
      - method: `ConfigureJWT`
  - DTOs
    - `UserCredentialsDto.cs`
  - Services
    - `IAuthenticationService.cs`
    - `AuthenticationService.cs`
  `Program.cs`
    - call `ConfigureJWT` 

