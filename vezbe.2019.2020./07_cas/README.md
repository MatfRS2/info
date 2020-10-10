# Вежбе -- Седми час -- Додавање функционалности корпи, регистровање сервиса и _Dependency Injection_, измене базе, _Model Binding_ и _Data Anotations_

[повратак](../../README.md)

**Наставак унапређивања и додавања нових функционалности интернет продавници.**

## Задатак 1: додати дугме за повратак из корпе, али тако да корисник оде на ону страну коју је претходно гледао

- Пре одласка на списак корпе потребно је запамтити (прецизније проследити) тренутни url. 
- Зато се прави још један View Model: `KorpaViewModel` који има два податка: корпа и url.
- Да би покупили исправан url потребно је позвати неколико метода. Лепо објашњење како се креира url може се погледати [овде](https://stackoverflow.com/questions/16675191/get-full-url-and-query-string-in-servlet-for-both-http-and-https-requests). У фолдеру `Infrastructure` креира се класа `UrlExtension`. Креирање класе није било неопходно, могли су сви позиви да се обаве и у оквиру _Razor_ странице. Ипак, овакав приступ се сматра добром праксом.
- [Документација](https://docs.microsoft.com/en-us/dotnet/api/system.web.httprequest?redirectedfrom=MSDN&view=netframework-4.8#properties) за `HttpRequest` класу (коришћена у оквиру UrlExtension)
- Додати линк у `SpisakKorpe.cshtml`, изменити `ProizvodIspis` тако да се прослеђује url и изменити методе `DodajUKorpu` и `SpisakKorpe` (тако да прослеђују url)

## Задатак 2: обрисати производ из корпе

- Додати дугме за брисање у `SpisakKorpe.cshtml`, додати метод `IzbrisiIzKorpe` у `KorpaController` и додати метод `ObrisiProizvod` у `Korpa` класи.

## Задатак 3: на врху стране додати информације о тренутном стању у корпи

- Ово је део стране, па се као и код навигације користи ViewComponent. Креирати `KorpaViewComponent`
- У `View->Shared->Components->Korpa->Default.cshtml` креирати неопходан испис за приказ тренутног стања корпе
- Додати _fontawesome_ иконицу: `Add --> Client Side Library --> cdnjs --> font-awesome --> Install`
- Позвати компоненту у оквиру _Views/\_Layout.cshtml_

---

**Посматрати на даље _Prodavnica\_poboljsanje_**

- Може се приметити да се у `KorpaController` и `KorpaViewComponent` више пута појављују методи `SetKorpa` и `GetKorpa` и овако понављање кода није добро.
- Зато би било добро податке о корпи преузимати на неки другачији начин, коришћењем сервиса.
- Креирати класу `SesijaKorpa` која наслеђује класу `Korpa`
- У оквиру ње направити непходне методе за рад са корпом (`DodajProizvod`, `ObrisiProizvod`, `ObrisiKorpu`)
- Обратити пажњу да ова класа има и поље `Session`  где се чувају подаци о тренутној сесији. То поље служи да када се направе измене над корпом (коришћењем горњих метода) те измене се запамте у оквиру сесије. Такође, обратити пажњу на атрибут `JsonIgnore` којим се заправо каже да се ово поље игнорише приликом чувања (односно серијализације) података коришћењем метода `Json.Serialize`. Ово је и логично понашање јер не желимо да у оквиру сесије памптимо податке о тој сесији (већ само податке о корпи).
- Најинтересантнији је метод `GetKorpa`:
	- Наиме, у оквиру контролера или компоненте се лако може приступити `HttpContext` објекту. Наиме, приликом креирања инстанце контролера или компоненте добијају се и подаци о `HttpContext` објекту. Зато смо то могли несметано да користимо када смо писали методе у `KorpaController` и `KorpaViewComponent`.
	- Ово је најобичнија класа и приступ `HttpContext` објекту одавде није могућ, већ морамо да кориситимо сервис да приступимо овом објекту.
	- Зато се позива метод `service.GetRequiredService<sta nam treba>`, а у овом примеру је потребан `IHttpContextAccessor`
	- Потом се у класи `Startup` региструје на који начин се приступа сервису `IHttpContextAccessor`. Наиме, дода се слеедћи део кода: `services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();`
	- Користи се `AddSingleton` јер је циљ да се кроз целу апликацију користи један објекат за `HttpContext` (чиме се имплицитно омогућава једна сесија по кориснику)
- Да би Корпа била доступна потребно је и њу регистровати преко сервиса. Односно у 	класи `Startup.cs` додати следеће: `services.AddScoped<Korpa>(sp => SesijaKorpa.GetKorpa(sp));`. Овим се каже да када год је потребан објекат типа `Korpa` она се добија коришћењем метода `SesijaKorpa.GetKorpa(sp)`.
- Сада се мењају класе `KorpaController` и `KorpaViewComponent`. Бришу се методи `GetKorpa` и `SetKorpa` и мењају се контруктори.
- **У реализацији овог примера интезивно је коришћена техника `Dependency injection`. Више о томе може се прочитати у званичној [документацији](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1).**
- **[Лепо (илустративно) објашњење у разлици између `AddTransient`, `AddSingleton` и `AddScoped`](https://stackoverflow.com/questions/38138100/addtransient-addscoped-and-addsingleton-services-differences).**

## Задатак 4: креирање и памћење поруџбина у бази

- Креирати класу `Porudzbina`. 
- Да би лакше повезали податке из _Razor_ странице са класом користимо технику _Model Binding_. Више о овоме у званичној [документацији](https://docs.microsoft.com/en-us/aspnet/core/mvc/models/model-binding?view=aspnetcore-3.1).
- Додатно, да би лакше озвестили о потенцијалним грешкама у току уношења података користимо _Data Anotations_. O различитим могућностима које се могу поставити, више се може прочитати [овде](https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/mvc-music-store/mvc-music-store-part-6). 
- Упозорења се стављају на страницу коришћењем:
	- `<div asp-validation-summary= .... >` или
	- `<span asp-validation-for=...`
- Додати линк за плаћање у `SpisakKorpe.cshtml` и креирати страницу `Placanje.cshtml`.
- Да би поруџбине могли да памтимо у бази у `ApplicationDbContext` додати поље `Porudzbine`. Затим се миграције морају изменити:
	- `dotnet ef migrations add Porudzbine`
	- `dotnet ef database update`
- Обратити пажњу на измене настале у бази. Додате су две табеле: Porudzbine i KorpaElement	
- Креирати `IPorudzbinaRepozitorijum` и `EFPorudzbinaRepozitorijum`, регистровати сервис у класи `Startup`
- Креирати `PorudzbinaController`. 


[повратак](../../README.md)