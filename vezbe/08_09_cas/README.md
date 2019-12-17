# Вежбе -- Oсми и девети час -- Креирање CRUD за производе, рад са фајловима, асихроне методе, Identity и ауторизација

[повратак](../../README.md)

**Наставак унапређивања и додавања нових функционалности интернет продавници.**

## Задатак 1: Додати могућност слања поруџбине 

- Додати поље `Poslato` u `Porudzbina`
- Додати у `PorudzbinaController` методе `SpisakNeposlatihPorucdzbina` и `OznaciKaoPoslato`
- Креирати `_AdminLayout.cshtml`
- Креирати `SpisakNeposlatihPorudzbina.cshtml`. Страни се може приступити преко `https://localhost:XXXX/Porudzbina/SpisakNeposlatihPorucdzbina`. Обратити пажњу да за сада никоме није ограничен приступ.

## Задатак 2: Додати могућности за брисање, додавање и мењање производа (CRUD -- create read update delete)

- Креирати `AdminController`, додати конструктор и метод `SpisakProizvoda`
- Креирати страницу `Views/Admin/SpisakProizvoda.cshtml`. Обратити пажњу на акције `Обриши` и `Измени`. На основу имплементације може се приметити да је `Измени` линк, док је `Обриши` решено преко форме. Разлог за ову разлику је у следећем:
	- `Измени` нас упућује на следећу страницу где се измене уносе. Корисник не уноси никакве податке кликом на дугме `Измени` (тј. не дешава се никаква _post_ акција).
	- `Обриши` мења стање апликације. Односно, корисник уноси податак о производу (то је заправо поље _hidden_) и примењује једну акцију. Заправо, имамо _post_ операцију и не може се задати преко линка.
- Додати методе `Obrisi` у `Controllers/AdminController` и `BrisiProizvod` у `EFRepozitorijum.cs`. Измене у репозиторијуму се дешавају тек након примене метода `SaveChanges` (све пре тога је локално).
- Направити измене у `_AdminLayout.cshtml` и додати испис за `TempData`. Подаци које чува `TempData` су доступни све док се не прочитају. То значи да ће бити приказани приликом приказа стране први пут након брисања, али ће нестати ако се страна освежи. Више о овоме, као и примере и начин коришћења погледати у званичној [документацији](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-3.1#tempdata).
	- У овом примеру није могуће користити _ViewBag_. У овом примеру, приликом брисања врши се редирекција на страну `SpisakProizvoda.cshtml` и то се третира као нови HttpRequest и _ViewBag_  подаци не могу да се преносе међу различитим захтевима (тј. трају само у оквиру једног захтева).
	- Са друге стране, подаци који се односе на једну сесију трају током трајања те сесије. То значи да ако би ове поруку желели да сачувамо и прикажемо коришћењем сесија, онда би та порука траја много дуже него што је потребно (и морала би ручно да се брише из сесије). `TempData` подаци нестају оног тренутка када се прикажу.
- У `AdminController` додати метод `Izmeni` (прво `get`)
- У `Models/Proizvod.cs` додати _data anotations_ и _model binding_ и креирати `Views/Admin/Izmeni.cshtml`. У овом примеру желимо да учитавамо слике, па је у оквиру форме потребно навести `multipart/form-data`.
- У `IProizvodRepozitorijum` и `EFRepozitorijum.cs` додати `SacuvajProizvod`. Ово је први пут да експлицитно пишемо асихрони метод. У принципу, савет је да сви методи у контролерима буду асихрони, а поготову они који чувају или читају податке из базе. Мали подсетник за асихроне методе:
	- Кључне речи **await** и **async** омогућавају да се лако пишу функције које су асихроне (а компајлер у позадини одрађује све што треба). Са **async** се наглашава да је функција асихрона (и да у себи има кључну реч **await**).
	- Кључна реч **await** се наводи испред позива методе којој треба време да се изврши.
	- [Добар чланак о асинхроним функцијама](https://exceptionnotfound.net/asynchronous-programming-in-asp-net-csharp-ultimate-guide/).
- У `AdminController` додати метод `Izmeni` (сада `post`). Креирати фолдер `wwwroot/ProizvodiSlike`. У методи `Izmeni` коришћен је атрибут `Bind`. Препорука је да се овај атрибут користи као један од нивоа заштите против _over-post_.
- Важна напомена: да би корисник могао да приступи неком фолдеру, онда је пожељно поставити га у `wwwroot` фолдер или експлицитно променити конфигурацију и навести где корисник још има права приступа.
- Овом приликом дата је могућност учитавања слика. Имплементирано решење је прилагођено оперативном систему Windows, али за друге системе би било потребно позвати друге функције (и користити друге библиотеке).
- Коришћен је `IHostingEnvironment` који омогућава приступ подразумеваној путањи (wwwroot фолдер). Наравно, у `Startup.cs` могуће је изменити све подразумеване путање (у Core 3.0 то је сада `IWebHostEnvironment`!).
- Више о учитавању и чувању фајлова у [документацији](https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-3.0).
- Постоји пуно дискусија на тему где је боље чувати документа -- у бази или у бази чувати само путању. Постоји неколико фактора који утичу на коначну одлуку, али се блага предност даје чувању путања (на интернету се може наћи пуно дискусија на ову тему и предности или мане једног или другог приступа). 
- Обрати пажњу на `_AdminLayout.cshtml` и `.input-validation-error` (означавање погрешног уноса)
- Додати `img` у `ProizvodIspis.cshtml`. Иначе, овај приступ није најбоље решење јер се директно шаље путања до фајла. Боље би било користити `IFileProvider`. Погледати званичну [документацију](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/file-providers?view=aspnetcore-3.1) и примере како би било исправно, рецимо нешто овако:
```csharp
services.AddSingleton<IFileProvider>(
                    new PhysicalFileProvider(Path.Combine
                    (Directory.GetCurrentDirectory(), "wwwroot/ProizvodiSlike")));   
```

---
## Задатак 3: Изменити права приступа

- За рад са кориницима користимо ASP.NET Core библиотеку Identity у којој је већ имплеметирано већина својстава који су најчешће потребни у апликацији. Више о разним могућностима које нуди у званичној [документацији](https://docs.microsoft.com/en-us/aspnet/identity/) (two-factor authentication, password recovery...).
- Креирати класу _MojKorisnik_, класу AppIdentityDbContext, у _appsetting.json_ додати _connection string_ за нову базу, и у класи `Startup` додати потребну конфигурацију (подешавања Enitity Framework-а за рад са Identity базом и подешавања самог Identity -- задавње која класа се користи за корисника (MojKorisnik), а која за роле (користимо уграђену класу IdentityRole))
- Потом:
	- `dotnet ef migrations add Initial --contex AppIdentityDbContext`
	- `dotnet ef database update --context AppIdentityDbContext`
- Креирати `AccountController`, `Views/Account/Korisnici.cshtml`, `Views/Account/KreirajKorisnika.cshtml`, `Models/ViewModels/KreirajKorisnikaModel.cs`
- У `AccountController` додати метод `KreirajKorisnika`, `Obrisi`
- Роле ћемо додати експлицитно у базу. То је опет могуће урадити на два начина, преко упита (рецимо, коришћењем упита су додати производи у базу на почетку прављења апликације) или директно из апликације. Овог пута је одабран други начин:
	- Направи `Models/IdentitySeedData.cs` (додајемо само две роле `Administrator` и `ObicanKorisnik`). Уколико ове роле већ постоје обнда се ништа не дешава, али ако не постоје додају се у базу.
	- На дну методе `Configure` у класи `Startup` додати `IdentitySeedData.DodajDefaultRole(app);`
	- Да не би избацивао грешку изменити метод `CreateWebHostBuilder` у `Program.cs`
- На разна места (код разних контролера и њихових метода) додати:
    - [Authorize (Roles = "Administrator")]
    - [Authorize (Roles = "Administrator, ObicanKorisnik")]
    - [AllowAnonymous] 
    - итд.
- У `AccountController` додати методe `Prijavljivanje` и `Odjava`
- Креирати `Views/Account/Prijavljivanje.cshtml` и додати могућност за одјаву у `_AdminLayout`
	

[повратак](../../README.md)