# Вежбе -- Први час -- Упознавање са C\#

[повратак](../../README.md)

**Текст на енглеском је преузет из различитих извора -- уџбеника, интернета итд.**


## Основе

 * The Common Language Runtime has a garbage collector that executes as part of your  program, reclaiming memory for objects that are no longer referenced. This frees programmers from explicitly deallocating the memory for an object, eliminating the problem of incorrect pointers encountered in languages such as C++.

 * The .NET Framework is organized into nested namespaces
 * The C# compiler compiles source code, specified as a set of files with the .cs extension.
 * Водити рачуна о показивачким променљивима (погледати пример у коду): класа, низ, интерфејс типови, делегати.
 * _Value types_:
    - Signed integer (```sbyte``` (8), ```short``` (16), ```int``` (32), ```long```(64))
	- Unsigned integer (```byte```(8), ```ushort```(16), ```uint```(32), ```ulong```(64))
	- Real number (```float```(32), ```double```(64), ```decimal```(128, где је потребна велика прецизност, записани су у декадном систему, а не у бинарном систему))
	- Logical (```bool```)
	- Character (```char```)
 * Специјалне вредности: `double.NaN`, `double.PositiveInfinity`, `double.NegativeInfinity`, `decimal.MaxValue`, ` double.MinValue`, ...
 * Стрингови (укратко): 
 	- пореде се по вредности, обратити пажњу на вербатим стрингове који почињу са `@` (видети пример у коду)
 	- `+` спаја два стринга
 	- _interpolated string_ почиње са $ и замењује свако појављивање променљивих у заградама. Уколико желимо да буде у више редова
 	 `$` мора бити испред `@`.
* Низови. Погледати код. Алоцираном низу се не може мењати димензија (о динамичким структурама касније). 
* Матрице: две врсте, нпр. ```int[,]``` и ```int[][]```. Погледати пример.	
* Пренос преко показивача (референце): користи се кључна реч `ref`. Постоји и `out` и користи се када није потребно проследити параметар функцији (најчеће за враћање више повратних вредности из функције). Такође, може се користити `_` ако нека повратна вредност није битна.
* Може се користити `params` уколико функција има променљив број параметара.
* Неименовани тип `var`.
* _Null_ оператор  `??`: ако вредност не постоји, подразумевано је ...
* _Elvis_ оператор `.?`: потребно код вредности које могу бити `null`, а чијем пољу желимо да приступимо.
* _switch_ изрази могу бити коришћени са типовима:
  ```csharp
	static void TellMeTheType (object x)   // object allows any type.
	{
	  switch (x)
	  {
	    case int i:
	      Console.WriteLine ("It's an int!");
	      break;
	    case string s:
	      Console.WriteLine ("It's a string");
	      break;
	    case bool b when b == true:     // Fires only when b is true
	      Console.WriteLine ("True!");
	      break;
	    default:
	      Console.WriteLine ("I don't know what x is");
	  }
	} 
  ```

* Директива `using` служи са укључивање различитих `namespace`.
* Директива `using static` служи за укључивање специфичног типа у оквиру неког `namespace`-a. Нпр. `using static System.Console` омогућва да користимо метод класе `Console` без навођења имена класе. Видети пример.

 ---
 
### Класе

* `readonly` -- вредност може бити мењана само у конструктору приликом иницијализације. Разликује се од `const` која за сваку инстацу има исту константну вредност, `readonly` за различите инстанце може да има различите вредности. 
* Могуће је задати почетну вредност неког поља у класи (та вредност се иницијализује пре позивања конструктора).
* Уколико метод има само `return` или једну наредбу, онда се може скратити. Нпр.
```csharp
   int primer(int x) { return x+1; }
```
је исто што и 
```csharp
	int primer(int x) => x + 1;
```

* Методи се могу исто звати уколико су им параметри различити.
* Ако није дефинисан ни један конструктор, онда је подразумеван празан конструктор. У конструкторима се може користити кључна реч `this` да означи позивање неког већ постојећег конструктора. `this` означава саму ту инстанцу.
* Постоји и деструктор, користи се кључна реч `Deconstruct`. Више о овоме у књизи. Постоји и finalizer, синтакса је `~ImeKlase(){}` и извршава се пре garbage collector.
* Иницијализација објеката приликом конструкције:
```charp
    public class Covek {
    	public string Ime;
    	public string Prezime;
    	public int GodinaRodjenja;
    }
    ....
	Covek pera = new Covek {Ime = Pera, Prezime = Kovac, GodinaRodjenja = 2000};
```	

* Погледати у примерима (и књизи) о гетерима и сетерима.
* Могуће је креирати индексе за неки тип коришћењем метода:

```csharp
public string this [int wordNum]
```

* Класа може бити `static` и то означава да може садржати само статичке елементе и да не може бити подкласа. Класа може бити и парцијална, али више о томе у књизи.
* Погледати пример са `as` оператором, биће коришћен касније (у ASP.NET Core примерима).
* Кључне речи `virtual` и `override` користе се за методе које могу бити измењене у класама наследницима.
* Користи се `abstract` за апстрактне класе и методе. Није могуће креирати инстанцу апстрактне класе.
* Више о кључним речима `sealed` и `new` за методе/поља класе се може прочитати у књизи.
* Кључна реч `base` служи да приступимо методама родитеља. Често се користи код конструктора.
* object (System.Object) is the ultimate base class for all types. Any type can be upcast to object.
* _Access Modifiers_:
	- `public`: Fully accessible.
	- `internal`: Accessible only within the containing assembly or friend assemblies.
	- `private`: Accessible only within the containing type.
	- `protected`: Accessible only within the containing type or subclasses.
* Interface: доста ћемо користити, најчеће у виду имплементирања постојећих интерфејса. За интерфејс важи:
	-  Interface members are all implicitly abstract. In contrast, a class can provide both abstract members and concrete members with implementations.
	- A class (or struct) can implement multiple interfaces. In contrast, a class can inherit from only a single class, and a struct cannot inherit at all.
  An interface declaration is like a class declaration, but it provides no implementation for its members, since all its members are implicitly abstract. These members will be implemented by the classes and structs that implement the interface.
  	- Use classes and subclasses for types that naturally share an implementation.
	- Use interfaces for types that have independent implementations.
* Генерички типови

### Напредни C\#

* _Делегати_(delegates): користе се приликом дефинисања метода и корисни су када метод преносимо као параметар функцији. Постоје уграђени (генерички) `Func` и `Action` који могу имати до 16 параметара. Мана им је што немају `ref` и `out` као могућност, па се у тим ситуацијама морају писати своји делегати. Погледати пример и прочитати у књизи више о делегатима. 
* _Ламбда изрази_: `(parameters) => expression-or-statement-block`. Ламбда изрази се користе уместо делегата када год је то могуће.
* _try-catch-finally_: механизам за обраду изузетака, више у књизи
* _Extension methods_: омогућава да се дода нова метода у већ постојећу класу. Врло корисно код већ готових класа које долазе уз .Net Core Framework. 
* _N-торке_(tuples)
* _Атрибути_: наводе се изнад класе, метода, поља, објекта .... и служе за додавање информација и ограничења. Током курса ћемо их интезивно користити, али више о томе касније.

--- 

## Преглед _Framework_-а

Преглед свега што постоји .Net Framework-у. У оквиру курса ће се радити .Net Core Framework и .ASP Net Core па се детаљи везани за други framework и друге могуућности у прављењу апликација могу прескочити.

---

### Основе _Framework_-а

* `Char`: `System.Char.` за функције са карактерима.
* `string`: функције за рад са нискама и могућности различитог енкодирања карактера
* Постоје `DateTime` и `DateTimeOffset`. Други узима у обзир временске зоне, док први не. Постоји неколико различитих конструктора, а најчеће се користи:
  ```csharp
  public DateTime (int year, int month, int day,
                 int hour, int minute, int second, int millisecond);
  ```
  Прва три параметра су обавезна, а друга три опциона. Више о овоме у књизи.
* Форматирање и парсирање, методи `ToString` и `Parse`.  
* `Math`, `Complex`, `Random`, `Guid`


---

### Колекције

* The types in the Framework for collections can be divided into the following categories:
	* Interfaces that define standard collection protocols
	* Ready-to-use collection classes (lists, dictionaries, etc.)
	* Base classes for writing application-specific collections

| Namespace                         | Contains |
| --------------------------------- |------------- |
| `System.Collections`              | Nongeneric collection classes and interfaces |
| `System.Collections.Specialized`  | Strongly typed nongeneric collection classes |
| `System.Collections.Generic`      | Generic collection classes and interfaces |
| `System.Collections.ObjectModel`  | System.Collections.ObjectModel |
| `System.Collections.Concurrent`   | System.Collections.Concurrent |

* Уз намерно прескакање неких детаља (може се погледати у књизи), све колекције интерпретирају интерфејс `IEnumerable<T>` који има метод `MoveNext`, а скраћено овај метод се позива преко `foreach` израза.

* `ICollection<T>` имплементира `IEnumerable<T>`, али има методе:
  * `Count` (пребројавање елемената, ово је поље)
  * `Contains` (да ли се елемент налази у колекцији)
  * `ToArray` (копирање колекције у низ)
  * `Add`, `Remove` и `Clear` за рад над елементима колекције

* `IList<T>` поред метода из `IEnumerable<T>` и `ICollection<T>` имплементира и пар метода за рад над индексима:
	* `IndexOf`, `Insert`, `RemoveAt`

* `Array` имплементира `ICollection`. Корисно у ситацијама када се величина низа не мења, јер је иначе врло неефикасно -- алоцира се нови простор и онда се садржај низа копира у тај нови простор. Често коришћене методе (поља):
	* `Length`
	* `Find`, `FindAll`
	* `Sort`

* `List<T>` имплементира интерфејс `IList<T>`. Ово је динамичка струкрура и елементи се могу додавати и брисати. Приликом уметања шифтују се сви елементи (да би се направио простор). У принципу, ово је паралела са динамичким низовима у `C`-у. Пуно корисних метода, ево неколико најчешће коришћених:
	* `Add`, `AddRange`
	* `RemoveAt`, `RemoveAll`
	* `Insert`

* `LinkedList<T>` представља једноструко повезану листу. Имплементира `ICollection<T>`, али не и `IList<T>` (није могућ приступ преко индекса).

* `Queue<T>` за ред. `Stack<T>` за стек.

* `HashSet<T>` и `SortedSet<T>`:
	* Their Contains methods execute quickly using a hash-based lookup.
	* They do not store duplicate elements and silently ignore requests to add duplicates.
	* You cannot access an element by position
	`SortedSet<T>` одржава низ сортираним након додавања новог елемента.
	
 	
* `IDictionary<TKey, TValue>` је интерфејс који служи за приказ _dictionary_. Постоји више различитих имплементација овог интерфејса (`Hashtable`, `OrderedDictionary`, `SortedDictionary <K,V>`, `Dictionary<TKey, TValue>`, итд.) који се разликују по брзини извршавања различитих операција, али најчеће је у употреби `Dictionary<TKey, TValue>`.

---

## LINQ

* _LINQ_ enables you to query any collection implementing `IEnumerable<T>`.  LINQ offers the benefits of both compile-time type checking and dynamic query composition.

* All core types are defined in the `System.Linq` and `System.Linq.Expressions` namespaces.

* Упити враћају `IEnumerable<T>` објекат и некада је потребно над резултатом применити `ToList` или `ToArray` (због лењог израчунавања ово превеђење је понекад неопходно).

* Упити се могу писати различитом синтаксом: _fluent syntax_ и _query expression syntax_. Погледати пример у коду. Чешће је у употреби први начин.	Компајлер преводи _query expression syntax_ у _fluent syntax_, а потом даље. Ове две синтаксе се могу комбиновати.

* _fluent syntax_: За креирање комплексних упита, потребно је надовезивати додатне операторе један на други и тако се ствара ланац.

* _query expression syntax_ се користи у следећим ситуацијама:
	* A let clause for introducing a new variable alongside the range variable
	* SelectMany, Join, or GroupJoin, followed by an outer range variable reference
	
* LINQ упити могу да раде са локалним објектима, тј. за колекције које су локалне и ово су _local queries_. Постоји и друга опција, а то је да раде са удањеним подацима (_remote data sources_), као што су базе података и овакви упити се називају _interpreted queries_.

* За  _interpreted queries_ користимо колекције које интерпретирају `IQuerable<T>`. `IQuerable<T>` je проширење (_extension_) `IEnumerable<T>` ( _Extension methods_ је помињано у Chapter 4).

* Када се користи `IQuerable<T>` онда се приликом прављења упита генерише _дрво израза_ (енг. _expression tree_). Постоје две имплементације за `IQuerable<T>` у оквиру .NET Framework-а:
	* LINQ to SQL. Предности су: једноставан за употребу, добре перформансе, квалитетно SQL превођење, згодан за учење
	* Entity Framework (EF) (у наставку курса ми ћемо користити `Entity Framework Core`, али више о томе касније). Предности су: веома флексибилан у креирању софистицираног мапирања између базе и класа. Подржва и друге системе осим SQL Servera. EF користи _Entity Data Model_(EDM) који се најчешће приказује у виду XML датотеке која служи као веза између класа и базе. Више о свему овоме касније током курса.

* Разлика приликом коришћења `IEnumerable<T>` и `IQuerable<T>` је у ефикасности извршавања. Наиме, `IQuerable<T>` се извршава на серверу, упит који је написан у програму се преводи у SQL упит који се онда изврши на серверу. То значи да се неће сви подаци довући са сервера већ само они који задовољавају упит. Са друге стране `IEnumerable<T>` се извршава локално. То значи да ће се сви подаци довући са сервера, а онда над њима ће бити извршен упит локално (детаљније је у књизи, овде намерно прескочени неки детаљи). 

* `AsEnumerbale()` је метод који кастује `IQueryable<T>` секвенцу у `IEnumerable<T>`. Посматрано са тачке извршавања: све до позива метода `AsEnumerable()` се извршава на серверу. А након тог позива ће се извршавати локално. Ово је понекад неопходно јер SQL Server не подржава све операције (нпр. регуларни изрази) и да би упит могао да се изврши потребно је да се део упита изврши локално (видети пример у коду). Уместо `AsEnumerable()` може се користити и `ToList()` или `Toarray()` (наравно, разлике постоје, а први начин је бољи због ефикасности). 

* Слично, само обрнуто ради `AsQuerable()`.

* Више о `Entity Framework Core` касније.

---

### LINQ Оператори

| Method             | Description |
| ------------------ |-------------------------------------------- | 
| `Where`            | Returns a subset of elements that satisfy a given condition | 
| `Take`             | Returns the first count elements and discards the rest |
| `Skip`             | Ignores the first count elements and returns the rest |
| `TakeWhile`        | Emits elements from the input sequence until the predicate is false |
| `SkipWhile`        | Ignores elements from the input sequence until the predicate is false, and then emits the rest |
| `Distinct`         | Returns a sequence that excludes duplicates |
| `Select`           | Transforms each input element with the given lambda expression |
| `SelectMany`       | Transforms each input element, and then flattens and concatenates the resultant subsequences |
| `Join`             | Applies a lookup strategy to match elements from two collections emitting a flat result set |
| `GroupJoin`        | As above, but emits a hierarchical result set |
| `Zip`              | Enumerates two sequences in step (like a zipper), applying a function over each element pair. |
| `OrderBy`, `ThenBy`| Sorts a sequence in ascending order. |
| `OrderByDescending`, `ThenByDescending` | Sorts a sequence in descending order |
| `Reverse`          | Returns a sequence in reverse order |
| `GroupBy`          | Groups a sequence into subsequences |
| `Concat`           | Returns a concatenation of elements in each of the two sequences |
| `Union`            | Returns a concatenation of elements in each of the two sequences, excluding duplicates |
| `Intersect`        | Returns elements present in both sequences |
| `Except`           | Returns elements present in the first, but not the second sequence |
| `First`, `FirstOrDefault` | Returns the first element in the sequence, optionally satisfying a predicate |
| `Last`, `LastOrDefault` | Returns the last element in the sequence, optionally satisfying a predicate |
| `Single`, `SingleOrDefault` | Equivalent to First/FirstOrDefault, but throws an exception if there is more than one match |
| `ElementAt`, `ElementAtOrDefault` | Returns the element at the specified positionException thrown |
| `DefaultIfEmpty`   | Returnsa single-element sequence whose value is default(TSource) if the sequence has no elements |
| `Count`, `LongCount` | Returns the number of elements in the input sequence, optionally satisfying a predicate |
| `Min`, `Max`       | Returns the smallest or largest element in the sequence |
| `Sum`, `Average`   | Calculates a numeric sum or average over elements in the sequence |
| `Aggregate`        | Performs a custom aggregation |
| `Contains`         | Returns true if the input sequence contains the given element |
| `Any`              | Returns true if any elements satisfy the given predicate |
| `All`              | Returns true if all elements satisfy the given predicate |
| `SequenceEqual`    | Returns true if the second sequence has identical elements to the input sequence |
| `Repeat`           | Creates a sequence of repeating elements |
| `Range`            | Creates a sequence of integers |

---

## Concurrency and Asynchrony

* Посебно обратити пажњу на _Principles of Asynchrony_ и на _Asynchronous Functions in C#_.

* A _synchronous operation_ does its work before returning to the caller.

* An _asynchronous operation_ does (most or all of) its work after returning to the
caller. Asynchronous methods are less common, and initiate concurrency, because work continues in parallel to the
caller. 

* Кључне речи **await** и **async** омогућавају да се лако пишу функције које су асихроне (а компајлер у позадини одрађује све што треба). Са **async** се наглашава да је функција асихрона (и да у себи има кључну реч **await**).
Кључна реч **await** се наводи испред позива методе којој треба време да се изврши.

* The **async** modifier can be applied only to methods (and lambda expressions) that return `void` or a `_Task` or `Task<TResult>`.

* [Добар чланак о асинхроним функцијама](https://exceptionnotfound.net/asynchronous-programming-in-asp-net-csharp-ultimate-guide/).


[повратак](../../README.md)
