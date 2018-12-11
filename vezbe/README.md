# Вежбе -- Развој софтвера 2 @ Математички факултет

[РС 2](../README.md)

Овај сајт садржи вежбе за предмет **Развој софтвера 2** на мастер студијама на Математичком факултету.

## Упутство за студенте

### Иницијална подешавања

* Инсталирати `git`
* Направити налог на `github`
* Направити фолдер на локалном рачунару, позиционирати се у фолдер и клонирати репозиторијум наредбом ```git clone https://github.com/MatfRS2/RS2.git```

### Упутства за инсталације 

**Напомена:** Упутства су за оперативни систем Ubuntu 18.04.

* [_Visual Studio Code_](https://code.visualstudio.com/Download?wt.mc_id=DotNet_Home)
* [Упутство за _SQL Server_ на _Ubuntu_ оперативном систему](http://www.maxtblog.com/2018/07/installing-ms-sql-server-in-ubuntu-18-04/)
* [Упутство за _.NET SDK_](https://www.microsoft.com/net/learn/get-started-with-dotnet-tutorial)
* Код окружења **Visual Stdio Code** инсталирати проширења (_Extensions_): C#, GitLens, vscode-pdf, Docker, Angular Snipets
* [Упутство за _Azure Data Studio_](https://azure.microsoft.com/en-us/updates/azure-data-studio-is-now-available/)
* [Упутство за _Node.js_](https://linuxconfig.org/how-to-install-node-js-on-ubuntu-18-04-bionic-beaver-linux)
* [Упутство за _Docker_](https://linuxconfig.org/how-to-install-docker-on-ubuntu-18-04-bionic-beaver)  
   Да би радило и за обичног корисника, потребно је променити дозволе:   
   ``` sudo groupadd docker ```  
   ``` sudo usermod -aG docker $USER ```  
   Излоговати се и проверити да ли све ради са командом: ```docker run hello-world```
* [Упутство за _Docker compose_](https://www.digitalocean.com/community/tutorials/how-to-install-docker-compose-on-ubuntu-18-04)
* Упутство за преузимање потребних _Angular_ пакета:   
  ```npm install --global @angular/cli@1.0.2``` (може се одабрати нека новија верзија)

## Вежбе

### **I час** -- Упознавање

* Инсталације, рад у `Visual Studio Code`
* `ASP.NET Core MVC` упознавање
  * [Пример 1](./01_cas/primer1/README.md) -- коришћење постојећег шаблона
  * [Пример 2](./01_cas/primer2/README.md) -- упознавање
  * [Пример 3](./01_cas/primer4/README.md) -- једноставан унос података (база у радној меморији)

### **II час** -- _C#_, _Razor_, _LINQ_

* [_C#_ -- нека интересантна својства](./02_cas/csharpPrimeri/README.md) 
* [_Razor_ -- основе](./02_cas/razorPrimeri/README.md)
* [_LINQ_ -- основе](./02_cas/linqPrimeri/README.md)

### **III час** -- повезивање са `SQL Serverom`, Тестирање

* [Пример 1](./03_cas/primer4/README.md) -- Повезивање апликације са `SQL Serverom`
* [Пример 2](./03_cas/UnitTesting/README.md) -- Основе тестирања

### **IV час** -- креирање веб продавнице

* [Пример](./04_cas/README.md) -- Креирање веб продавнице

### **V час** -- креирање веб продавнице (додавање корпе)

* [Пример](./05_cas/README.md) -- Креирање веб продавнице

### **VI час** -- креирање веб продавнице (додавање поруџбине)

* [Пример](./06_cas/README.md) -- Креирање веб продавнице

### **VII час** -- додавање администраторских операција (_CRUD_)

* [Пример](./07_cas/README.md) -- Креирање веб продавнице

### **VIII час** -- Обезбеђивање различитих права приступа 

* [Пример](./08_cas/README.md) -- Креирање веб продавнице

[РС 2](../README.md)
