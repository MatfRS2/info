# Вежбе -- Трећи час -- Почетак рада на апликацији веб продавнице

[повратак](../../README.md)

**Почетак рада на апликацији веб продавнице. Наставак унапређивања и додавања нових функционалности овој апликацији је предвиђен током наредних часова.**


Први кораци:
* Кренути од празног пројекта: `New Project --> ASP.NET Core Web Application --> Empty Project`
* Додати потребну конфигурацију у `Startup.cs` 
* Направити фолдере `Models`, `Views` и `Controllers`
* Додати једноставан `_ViewImports`

## Неколико речи о конфигурацији пројекта (преглед датотеке _*.csproj_):

Кликом на име пројекта у **Solution Explorer** прозору (`ImeProjekta.csproj` датотека) може се видети тренутна конфигурација пројекта:
- верзија .Net Core која се користи, 
- фолдери који су део пројекта, 
- као и NuGet пакети који су део пројекта.
- 
Иако је могуће овај фајл мењати ручно углавном није потребно, већ се садржај додаје аутомаски.
Поред ручног додавања пакете је могуће додати и овако: 

` Tools --> NuGet Package Manager --> Manage NuGet Packages for Solution`

И у оквиру тог прозора могуће је претраживати и додавати неопходне пакете.

Oбратити пажњу да је неопходно имати везу са интернетом јер се пакети скидају са интернета и додају у пројекат.

Када се оде на опције пројекта у делу Debug моеже се видети да ли је у `Development`, `Staging` 
или `Deployment` фази. Заправо може се ставити шта год желимо и потом се то користи током
писања програма (обратити пажњу на `if (env.IsDevelopment())` у `Startup.cs`).

## Program.cs

Преузето из уџбеника:

The Main method provides the entry point that all .NET applications must
provide so they can be  executed by the runtime. The Main method in the
Program class calls the BuildWebHost method, which is  responsible for
configuring ASP.NET Core.

The BuildWebHost method uses static methods defined by the WebHost class to
configure ASP.NET Core.  With the release of ASP.NET Core 2, the configuration
is simplified by the use of the CreateDefaultBuilder  method, which configures
ASP.NET Core using settings that are likely to suit most projects. The
UseStartup  method is called to identify the class that will provide
application-specific configuration; the convention  is to use a class called
Startup. The Build method processes all the  configuration settings and
creates an object that implements the IWebHost interface, which is returned to
the  Main method, which calls Run to start handling HTTP requests.

## Startup.cs (методе _ConfigureServices_ и _Configure_)

Приликом покретања креира се инстанца ове класе и прво се позива метод `ConfigureServices`.

У методи `ConfigureServices` креирају се објекти који обезбеђују разне функционалности, које 
је потребно креирати у старту и који би требало да су видљиви кроз целу апликацију.

Потом се позива метод `Configure` у коме се смештају компоненте које се називају још и _middleware_.

Представљају ланац којим се заправо контролише извршавање захтева. Наиме, захтев стигне са 
_pipeline_ и обрађује се редом како је _middleware_ наведен. _Middleware_ може да проследи захтев следећем у низу или
да га обради и врати одговор.  _Middleware_ (у оквиру својих позива) може да користи сервисе које смо навели у методи `ConfigureServices`.

## Креирање UnitTest

`Add->New Project -->xUnitTest`

Задати ново име и одабрати `Add to this solution`.

Десни клик на име пројекта па `Manage NuGet Packages`, одабрати `Moq` и `Install`.

Десни клик па `Add --> Reference` и онда одабрати жељени пројекат (обратити пажњу на измене у `.csproj` фајлу).

## Даљи кораци

* Креирати класу `Proizvod`
* Креирати интерфејс `IProizvodRepozitorijum`. Интерфејс нам није неопходан, али је добра прилика да се види како се користи. Такође, због тестирања веома је корисно имати интерфејс (уместо да се ради само са базом).
* Присетити се шта је `IQueryable` -- веома лепо објашњење се може наћи у књизи C# 7.0 in a Nutshell (погледати први час вежби)
* У `Startup-->Configuration` додати `services.AddTransient<IProizvodRepozitorijum, LazniRepozitorijum>();`
* Направити контролер `ProizvodController` са једноставним методом `Spisak` који излистава све производе.
* Креирати `Views/Shared/_Layout.cshtml` и `Views/_ViewStart.html`
* Креирати `Views/Proizvod/Spisak.cshtml`
* Додати _default_ руту у `Startup.cs`

## Entity Framework Core

Преузето из уџбеника:

_Entity Framework Core -- also known as EF Core -- is an object-relational mapping (ORM) package produced 
by Microsoft that allows .NET Core applications to store data in relational databases.
Entity Framework Core has one key task: storing.NET objects in a database and retrieving them again later. 
Put another way, Entity Framework Core acts as the bridge between an ASP.NET Core MVC application and 
a database._


* Направити `ApplicationDbContex` и `EFRepozitorijum`
* Направити `appsettings.json` и додати податке потребне за `connection string`. Да би ови подаци (тј. конфигурација) били
препознати неопходно је поставити конфигурацију и у класи `Startup`.

* Најлакше је користити командну линију за рад са базом. Да би то омогућили прво у `Prodavnica.csproj` додати:
```csharp
<DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="2.0.0" />
```

Потом покренути командну линију `Tools-->Command Line-->Developer Command Prompt` и можемо куцати различите наредбе, нпр.

`dotnet ef migrations add Initial`

`dotnet ef database update`

Нешто више речи о миграцијама (из уџбеника):
|| |
| ------------------ |-------------------------------------------- |
|What are they?      |Migrations are groups of commands that prepare databases for use with Entity Framework Core applications. They are used to create the database                     and then keep it synchronized with changes in the data model.|
|Why are they useful?|Migrations automate the process of creating and maintaining databases for storing application data. Without migrations, you would have to create the                     database using SQL commands and manually configure Entity Framework                       Core to use it.|
|How are they used?  |Migrations are created and applied using the _dotnet ef command-line tools_.|

Наредбе које се односе на миграције и EF Core (из уџбеника):
|Ефекат      | Наредба |
| ------------------ |-------------------------------------------- |
|Create a new migration                                 | `dotnet ef migration add ime` |
|See the changes that a migration contains              | `dotnet ef migrations script`  |
| Apply a migration to the database                     | `dotnet ef database update`    |
| List the migrations in a project                      | `dotnet ef migrations list`    |
| Remove a migration                                    | `dotnet ef migrations remove`  |
| Reset the database                                    | `dotnet ef database update`   |
|                                                      |    `dotnet ef database drop`      |



**Важно:** Пре покретања апликација база мора да се попуни (_seed database_) неким подацима (иначе не ради). Може се урадити из C#, а може и помоћу SQL-а. Други метод је одабран за час вежби (погледати `seed_za_bazu.sql`).

---
## Stilizovanje

`Add --> Client Side Library --> jsdelivr --> bootstrap --> Install`

Додати стил у html тагове.

[повратак](../../README.md)