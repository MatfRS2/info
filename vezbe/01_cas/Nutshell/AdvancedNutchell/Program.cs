using System;
using System.Linq; //da bi mogli da koristimo LINQ-u upite.
using System.Collections.Generic; // treba nam zbog IEnumerable
using System.Text.RegularExpressions; //koristimo regularne izraze

namespace AdvancedNutchell
{
    class Program
    {
        static void Main(string[] args)
        {
            // primer nekog IEnumerable<T> objekta
            string[] imena = { "Petar", "Jovana", "Marko", "Isidora", "Nebojsa", "Ana",
                               "Sreahinja"};

            // povratna vrednost je IEnumerable<T>
            // moze se primeniti na sve IEnumerable<T> objekte

            //ovo je fluent syntax, cesce u upotrebi
            IEnumerable<string> imena1 = imena.Where(n => n.Length >= 4);

            foreach (string ime in imena1)
                Console.WriteLine(ime);
            Console.WriteLine();

            //ovo je query expression syntax, redje u upotrebi
            IEnumerable<string> imena2 = from ime in imena
                                         where ime.Contains('r')
                                         select ime;

            foreach (string ime in imena2)
                Console.WriteLine(ime);
            Console.WriteLine();

            /* ------- Zadatak 1. ---- */
            //Izdvojiti sve koji sadrze slovo r, sortirati po duzini i sva slova 
            //pretvoriti u velika
            //Select sluzi da transformise ulazu sekvencu na osnovu lambda izraza
            imena1 = imena.Where(n => n.Contains('r')).
                           OrderBy(n => n.Length).Select(n => n.ToUpper());

            foreach (string ime in imena1)
                Console.WriteLine(ime);
            Console.WriteLine();

            /* ------- Lenjo izracunavanje upita. (deffered execution) ---- */
            //Upiti se izvrasavaju tek kada je to potrebno. 
            Console.WriteLine("----------------------------");

            int faktor = 10;
            List<int> brojevi = new List<int>{1, 2, 3};
            var upit = brojevi.Select(n => n * faktor);

            foreach(int br in upit)
                Console.Write(br + " ");
            
            Console.WriteLine();
            faktor = 20;
            foreach(int br in upit) 
                Console.Write(br + " ");                

            Console.WriteLine();
            brojevi.Clear();
            foreach(int br in upit) 
                Console.Write(br + " ");   

            //Da bi se izbegli ovakvi problemi potrebno je rezultat prebaciti u listu ili niz (tada se rezultat izracunava i kopira)     
            List<int> brojevi2 = new List<int>{1, 2, 3};
            List<int> upit2 = brojevi2.Select(n => n * faktor).ToList(); //rezultat je isti bez obzira na izmene u nizu.                    

            /* ------- Zadatak 2. ---- */
            Console.WriteLine("----------------------------");
            //sortirati prema prezimenima
            string[] pesnici = {"Djura Danicic", "Vasko Popa", "Aleksa Santic", "Jovan Ducic", "Dobrica Eric"};

            var sortirani = pesnici.OrderBy(n => n.Split().Last());

            foreach(string pesnik in sortirani)
                Console.Write(pesnik + "| ");
            Console.WriteLine();

            //izdvojiti one pesnike cije prezime je najkrace
            Console.WriteLine("----------------------------");
            /*var kratko_prezime = pesnici.
            Where( n => n.Split().Last().Length == 
                        pesnici.OrderBy(m => m.Split().Last().Length).
                                First().Split().Last().Length); //ovaj deo moze i ovako: .Select(n => n.Split().Last().Length).Last()
            */
            //efikasnije
            var kratko_prezime = pesnici.Where( n => n.Split().Last().Length == pesnici.Min(m => m.Split().Last().Length));
            foreach(string pesnik in kratko_prezime)
                Console.Write(pesnik + "| ");
            Console.WriteLine();

            //izbaciti samoglasnike iz stringova
            //zabavno: koristimo regularne izraze: zameni aeiou sa praznim
            var bez_samoglasnika = pesnici.Select( n => Regex.Replace(n, "[aeiou]", ""));
            foreach(string pesnik in bez_samoglasnika)
                Console.Write(pesnik + "| ");
            Console.WriteLine();

            //TODO probaj
            //kreiranje novih objekata?
            var duzine_i_imena = pesnici.Select(n => new {Duzina = n.Length, Prezime = n.Split().Last()});
            foreach(var pesnik in duzine_i_imena)
                Console.WriteLine(pesnik);


            //primer sa SelectMany: koristimo kada imamo vise od jednog izlaza po sekvenci
            var imena_i_prezimena = pesnici.SelectMany(n => n.Split());
            foreach (var pesnik in imena_i_prezimena)
                Console.Write(pesnik + "| ");
            Console.WriteLine();

            //NAPOMENA: Operatori za sortiranje (OrderBy, ...) ne vracaju IEnumerable<T>
            //vec IOrderedEnumerable<T>
            //Ukoliko zelimo da na rezultat dobijen sa OrderBy primenimo Where (i slicno)
            //potrebno je prvo uraditi AsEnumerable()
        }
    }
}
