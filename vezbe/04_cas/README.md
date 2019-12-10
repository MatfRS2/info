# Вежбе -- Четврти час -- Креирање _tag-helper_-a и основе тестирања

[повратак](../../README.md)

**Наставак унапређивања и додавања нових функционалности интернет продавници.**

**Задатак:** На страни коју посматра корисник су приказани сви производи који постоје у бази. Желимо да изменимо приказ тако да на свакој страни буде приказано **_n_** производа и да корисник лако може да се креће по странама и прегледа све производе.

### Почетни кораци:

- У _ProizvodController.cs_ додаје се параметар _VelicinaStrane_ којим се задаје колико производа се приказује на свакој страни.
- Мења се метод _Spisak_ (_Resenje1_ у коментарима) коме се додаје додатни параметар _tekucaStrana_ на основу ког се врши исписивање производа
- Приликом покретања да би се приступило другој страни производа: `https://localhost:broj/?tekucaStrana=2`

### Tag helpers:

- [Документација](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/authoring?view=aspnetcore-3.0)
- Желимо да омогућимо да корисник може да кликне на линк који га води ка другој, трећој... страни производа (а не да ручно куца адресу)
- За ту сврху ћемо креирати _tag helper_
- _Tag helper_ приликом превођења мењају HTML елементе у погледу (view)
- Постоје уграђени (и неке ћемо и ми користити)
- А могу се писати и сопствени _tag helper_-и. 
- _Tag helper_-и се смештају у `Infrastructure/TagHelpers` фолдер.
- У пројекту је креиран _PageLinkTagHelpers_ -- погледати ову класу и обратити пажњу на коментаре који објашњавају сваки део кода.
- У _Spisak.cshtml_ се додаје: `<div model-strane="@Model.ModelStrane" akcija="Spisak"></div>`. Када се преведе биће замењен са `<a href...` таговима (погледати `inspect element` у браузеру)

### View Models:

- Поред класа којима се моделују подаци, понекад су потребне класе које моделују оно што се преноси у погледе (_razor_ странице).
- У овом примеру направљене су две такве класе: _PodaciZaPrikazStrane_ и _SpisakProizvodaViewModel_
- Класа _PodaciZaPrikazStrane_ се користи да се у њу сместе подаци потребни за _tag helper_ који је написан (погледати коментаре у коду)
- Класа _SpisakProizvodaViewModel_ се користи да би се _razor_ страници _Spisak_ пренели сви потребни подаци (низ производа + _PodaciZaPrikazStrane_)
- Мења се метод _Spisak_ (_Resenje2_ у коментарима) и _razor_ страници се прослеђују мало другачији параметри

## Тестирање

- Важан део сваког пројекта је и тестирање. Са развојем кода требало би паралелено развијати тестове. 
- Креирање UnitTest: `Add->New Project -->xUnitTest`
- Задати ново име и одабрати `Add to this solution`.
- Десни клик на име пројекта па `Manage NuGet Packages`, одабрати `Moq` и `Install`.
- Десни клик па `Add --> Reference` и онда одабрати жељени пројекат (обратити пажњу на измене у `.csproj` фајлу).
- Погледати примере тестова: _UnitTest1_ и _ProizvodControllerTests_
- Тестирати: `Test--> Test Explorer --> Run`

[повратак](../../README.md)