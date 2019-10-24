# Вежбе -- Други час -- Упознавање са `ASP.NET Core MVC`, рад са _Razor_ страницама

[повратак](../../README.md)


**Текст на енглеском је преузет из различитих уџбеника!!!**

## Основе ASP.NET Core MVC

**ASP.NET Core MVC** is a web application development framework. **ASP.NET Core** is built on **.NET Core**, which is a cross-platform version
of the .NET Framework without  the Windows-specific application programming
interfaces (APIs). Windows is still a dominant operating  system, but web
applications are increasingly hosted in small and simple containers in cloud
platforms,  and by embracing a cross-platform approach, Microsoft has extended
the reach of .NET, made it possible  to deploy ASP.NET Core applications to a
broader set of hosting environments, and, as a bonus, made it  possible for
developers to create ASP.NET Core web applications on Linux and macOS. ASP.NET
Core MVC Is Open Source.


**ASP.NET Core MVC** follows a pattern called model-view-controller, which
guides the shape of an ASP.NET  web application and the interactions between
the components it contains.

---
### МПК шаблон (Модел - Поглед - Контролер) (енг. _MVC pattern_)
In high-level terms, the MVC pattern means that an MVC application will be
split into at least three pieces:
* _Models_, which contain or represent the data that users work with
* _Views_, which are used to render some part of the model as a user interface
* _Controllers_, which process incoming requests, perform operations on the model, and 
select views to render to the user

---

## Креирање пројекта, основе _Razor_ страница

_Креирање пројекта:_

`New Project --> ASP.NET Core Web Application --> Empty Project`

Иако _Visual Studio_ нуди много шаблона препорука је кренути од празног пројекта и додавати потребне фајлове, фолдере ....

У конфигурацији (`ConfigureServices` и `Configure`) подесети потребне параметре за коришћење _MVC_ модела.

---

Направити фолдере _Models_, _Controllers_, _Views_ и додати потребне класе и
_Razor_ страницу. 

Прочитати о **повратним вредностима метода у контролеру**
[овде](https://stackoverflow.com/questions/4743741/difference-between-viewresult-and-actionresult).

### Конфигурационе _Razor_ странице

_Razor_ странице које почињу са \_ су конфигурационе стране које се не приказују кориснику.

Потом додати `_ViewImports.cshtml` која служи да се задају сви _namespace_
који се користе за приказ _Razor_ страница. `_ViewImports.cshtml` се смешта у
`Views` фолдер.


Потом додати `_MojLayout.cshtml` која служи да се зада _layout_, односно
делови приказа који су исти за све странице. Може бити више _layout_ у једној
апликацији. `_MojLayout.cshtml` се смешта у `Views/Shared` фолдер.

Додати и `_ViewStart.cshtml` стрицу у `Views`.

Синтакса и могућности које нуде _Razor_ странице погледати
[овде](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-3.0).



[повратак](../../README.md)