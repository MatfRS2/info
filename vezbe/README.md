# Вежбе -- Развој софтвера 2 @ Математички факултет Универзитета у Београду

[РС 2](../README.md)

Овај сајт садржи вежбе за предмет **Развој софтвера 2** на мастер студијама на Математичком факултету.

## Упутство за студенте

### Литература

Литература која покрива развој ASP.NET Core MVC апликација има веома много и на интеренту се могу наћи многи наслови који покривају различите нивое знања и потреба. У _literatura.zip_ се налази пар одабраних књига. Шифра за отварање архиве је _rayvojsoftvera2_.

### Иницијална подешавања

* Инсталирати `git`
* Направити налог на `github`
* Направити фолдер на локалном рачунару, позиционирати се у фолдер и клонирати репозиторијум наредбом ```git clone https://github.com/MatfRS2/RS2.git```

### Упутства за инсталације (оперативни систем Windows)

* **[Инсталирати GIT](https://git-scm.com/download/win)** за Windows (команде у терминалу су исте као и у Linux системима). 
  [Основне git команде](https://confluence.atlassian.com/bitbucketserver/basic-git-commands-776639767.html). 

* Инсталирати **[Visual Studio IDE](https://visualstudio.microsoft.com/)** (изабрати Comunity). Након месец дана пробне верзије ће тражити да се обнови лиценца. Само кликнути _update licence_ и добија се лиценца за бесплатно коришћење.
  Погледати [упутство](https://docs.microsoft.com/en-gb/visualstudio/get-started/csharp/tutorial-aspnet-core-ef-step-01?view=vs-2019).
  
* Током инсталације одабрати: _ASP.NET and Web development_ и _.NET Core cross-platform development_.
  Могу се одабрати и друге опције, али ове су неопходне.

* [Пречице](https://code.visualstudio.com/shortcuts/keyboard-shortcuts-windows.pdf) за _Visual Studio_.

* Инсталираи **[SQL Server](https://www.microsoft.com/en-ie/sql-server/sql-server-downloads)**. Бесплатне опције су Express и Development. Development има готово све могућности које нуди Enterprise, али није могуће користити за апликације које желимо да пустимо у рад. Са друге стране уз Express je могуће пустити у рад апликацјиу али нуди много мање опција.

* _SQL Server_: Поред администратора добро би било направити неког корисника који може да додаје/брише базе (и у њима додаје/брише табеле, податке итд.), али нема сва администраторска права. Такође, обратити пажњу на подешавања сервера јер је могуће да као подразумевану опцију дозвољава само логовоња коришћењем _windows login_. Могуће је променити опције, прочитати више [овде](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/change-server-authentication-mode?redirectedfrom=MSDN&view=sql-server-ver15).

* Да би лакше администрирали сервер и мењали базу добро би било инсталирати помоћне алате. На пример:
	* [SQL Server Management Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15)
	* [Azure Data Studio](https://docs.microsoft.com/en-us/sql/azure-data-studio/download?view=sql-server-ver15)


## Вежбе

### **I час** -- Упознавање са C\#

* Инсталације, рад у `Visual Studio Code`
* [_C#_ -- основе](./01_cas/README.md) 

### **II час** -- Упознавање са `ASP.NET Core MVC`, рад са _Razor_ страницама

* [Пример](./02_cas/README.md)

### **III час** -- Креирање веб продавнице, повезивање са `SQL Serverom`

* [Пример](./03_cas/README.md) -- Креирање веб продавнице

[РС 2](../README.md)
