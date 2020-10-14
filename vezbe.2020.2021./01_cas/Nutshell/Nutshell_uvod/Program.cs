using System;
using System.Globalization; //potrebno za CultureInfo
using static System.Console;
using System.Collections.Generic;

namespace Nutshell_uvod
{
    public class Point
    {
        public int x, y;
        /* Ovo je privatno polje. */
        string ime;

        /* konstruktor */
        public Point() { }
        public Point(int a, int b)
        {
            x = a; y = b;
        }

        /* Dodaju se geteri i seteri za ime. */
        public string Ime
        {
            get { return ime; }
            set { ime = value; }

            //Moze i ovako:
            //get => ime;
            //set => ime = value;
        }

        /* Najcesce se pise ovako: */
        /* Brzo generisanje: otkucati prop, pa dva puta tab */
        /* I get i set mogu biti private (ili bilo sta drugo). */
        public string Opis { get; set; } = @"Ovo je default vrednost koja moze da se menja. 
                                             Nije neophodno da postoji.";
    }

    /* ----------- Nasledjivanje ---------------- */
    public class Objekat
    {
        public string Ime { get; set; }

        public virtual void Ispis()
        {
            Console.WriteLine(Ime);
        }
    }

    public class Trougao : Objekat
    {
        public Point[] temena = new Point[3];

        public int BrojTemena() => 3;

        public override void Ispis()
        {
            Console.WriteLine(Ime + BrojTemena().ToString());
        }
    }

    public class Cetvrougao : Objekat
    {
        public Point[] temena = new Point[4];
    }

    public abstract class Vozilo
    {
        public Vozilo()
        {
            Console.WriteLine("Konstruktor roditelja");
        }
        public abstract string Tip { get; }
        public void Ispis()
        {
            Console.WriteLine("Tip vozila je " + Tip);
        }
    }

    public class Auto : Vozilo
    {
        public Auto()
            : base() //poziva se konstruktor roditelja
        {
            base.Ispis(); //poziva se metoda roditelja
            Console.WriteLine("Konstruktor deteta, odnosno Auto");
        }
        public override string Tip => "auto";
    }

    /* ------- Genericka klasa. T moze biti zamenjeno sa bilo kojim tipom. ---- */
    // Moguce je navesti i vise tipova
    // npr. public class Podaci<T1, T2, T3>
    public class Podaci<T>
    {
        public Podaci() { }
        public Podaci(T parametar)
        {
            Parametar = parametar;
        }

        public T Parametar { get; set; }

        public void Stampaj()
        {
            Console.WriteLine(Parametar);
        }
    }

    /* ------- Extension method. ---- */
    //prosiruju funkcionalost postojecih klasa dodavanjem novih metoda
    //korisno za koriscenje kod vec postojecih (.NET Core Framework) klasa
    //mora static
    public class Covek
    {
        public string Ime { get; set; }

        public void Stampaj()
        {
            Console.WriteLine(Ime);
        }
    }

    public static class Student
    {
        public static void Stampaj2(this Covek cv)
        {
            Console.WriteLine("Ovo je ispis iz extension method.");
            Console.WriteLine(cv.Ime);
        }
    }

    class Program
    {
        static void promeni(ref int x)
        {
            x = 10;
        }

        static void promeni2(int x, out int y1, out int y2)
        {
            y1 = x + 1;
            y2 = x - 1;
        }

        //Opcioni parametar mora na kraju
        static void promeni3(out int y1, out int y2, int x = 200)
        {
            y1 = x + 1;
            y2 = x - 1;
        }

        //Metode bez tela
        static int izracunaj(int x) => x + 1;

        /* ----------- Nasledjivanje, polimorfizam ---------------- */
        static void ispis_imena(Objekat o)
        {
            Console.WriteLine(o.Ime);
        }

        /* ------- Delegati. --------- */
        public delegate int Delegat1(int x);
        public delegate T Delegat2<T>(T x);

        public static int Kvadrat(int x)
        {
            return x * x;
        }

        public static int Kub(int x)
        {
            return x * x * x;
        }

        public static double Koren(double x)
        {
            return Math.Sqrt(x);
        }

        //Koriscenje delegata koje smo mi pisali
        public static int Izracunaj(Delegat1 f, int x)
        {
            return f(x);
        }

        public static T Izracunaj2<T>(Delegat2<T> f, T x)
        {
            return f(x);
        }

        //Ovo je ugradjeni delegat Func<in T, out T>, moze imati do 16 parametara
        //Postoji i Action<T, ...> koji nema povratnu vrednost
        public static T Izracunaj3<T>(Func<T, T> f, T x)
        {
            return f(x);
        }

        /* ------- Tuples. --------- */
        static (string, int, char) GetBob() => ("Bojan", 23, 'M');

        static void Main(string[] args)
        {
            int x = 12 * 30;
            Console.WriteLine(x);

            string poruka = "primer";
            poruka = poruka.ToUpper() + " " + x.ToString();
            Console.WriteLine(poruka);

            /* ----------- Voditi racuna o pokazivackim tipovima. ----------- */
            Point tacka1 = new Point(3, 4);
            Point tacka2 = tacka1;

            tacka1.x = 10;
            Console.WriteLine(tacka1.x);
            Console.WriteLine(tacka2.x);

            tacka2.y = 8;
            Console.WriteLine(tacka1.y);
            Console.WriteLine(tacka2.y);

            Console.WriteLine("-----------");
            /* ----------- Tipovi podataka, pogledati knjigu za informacije. ----------- */
            Console.WriteLine(double.MaxValue);
            Console.WriteLine(decimal.MaxValue);
            Console.WriteLine("-----------");

            /* ----------- Stringovi. ----------- */
            string a = "test";
            string b = "test";
            /* Poredjenje je po vrednosti. */
            Console.WriteLine(a == b);

            // specijalni znak @
            a = @"mogu da
                  stavim i novi red 
                  i specijalne karaktere $, \, ";
            Console.WriteLine(a);
            // Dva puta " da bi se ispisao znak "
            string xml = @"<customer id=""123""></customer>";
            Console.WriteLine(xml);

            /* ----------- Nizovi. Prilikom inicijalizacije svi elementi niza su na pocetku 0. ----------- */
            int[] niz = new int[5];
            niz[3] = 8;

            Point[] niz_tacaka = new Point[10];
            // Ovaj red izaziva gresku.
            // niz_tacaka[3].x = 5;
            // Mora ovako:
            for (int i = 0; i < 10; i++)
                niz_tacaka[i] = new Point();
            niz_tacaka[3].x = 5;

            /* ----------- Visedimenzioni nizovi. ----------- */
            //ovo je matrica 4x3
            int[,] matrica1 = new int[4, 3];
            matrica1[2, 2] = 8;

            //matrica sa 3 reda, u svakom redu je niz
            int[][] matrica2 = new int[3][];
            //za svaki red se alocira proizvoljan prostor, ne mora da bude isti broj
            for (int i = 0; i < 3; i++)
            {
                matrica2[i] = new int[i + 1];
                matrica2[i][0] = i;
            }

            /* ----------- Reference, koriscenje ref i out i podrazumevani parametri. -----------*/
            Console.WriteLine("-----------");
            Console.WriteLine("Reference, koriscenje ref i out.");
            x = 200;
            int izlaz1, izlaz2;
            promeni(ref x);
            Console.WriteLine(x);

            promeni2(x, out izlaz1, out izlaz2);
            Console.WriteLine(izlaz1);
            Console.WriteLine(izlaz2);

            // Promenljiva izlaz3 je deklarisana prilikom poziva!
            // Treca promenljiva je nebitna.
            promeni2(x, out int izlaz3, out _);
            Console.WriteLine(izlaz3);

            //Opcioni parametar se ne mora navesti
            promeni3(out izlaz3, out _);
            Console.WriteLine(izlaz3);

            /* ----------- Null i Elvis operator ----------- */
            Console.WriteLine("-----------");
            string s1 = null;
            string s2 = s1 ?? "nema vrednost!";
            Console.WriteLine(s2);

            string s3 = s1?.ToUpper();
            //int ne moze da ima vrednost null
            //ali int? moze
            int? poredjenje = s1?.CompareTo("nn");
            Console.WriteLine(s3 ?? "nema stringa!");
            if (poredjenje == null)
                Console.WriteLine("nema stringa, nema poredjenja");
            else
                Console.WriteLine(poredjenje);

            /* ----------- foreach ----------- */
            Console.WriteLine("-----------");
            foreach (char c in "rs2")
                Console.WriteLine(c);

            /* ----------- using static omogucava sledeci red ----------- */
            Console.WriteLine("-----------");
            WriteLine("ne treba Console.");

            /* ----------- Metode bez tela -------------- */
            Console.WriteLine("-----------");
            Console.WriteLine(izracunaj(11));

            /* ----------- Inicijalizacija objekata ---------- */
            // Pravi se nova tacka i dodeljuju se vrednosti poljima
            // Moze da se napravi i konstruktor koji to radi, ali ovako je omogucena
            // veca fleksibilnost
            Point tacka_ab = new Point { x = 10, y = 12 };

            /* ----------- Geteri i seteri --------------- */
            Console.WriteLine("-----------");
            tacka_ab.Ime = "A";
            Console.WriteLine(tacka_ab.Ime);
            Console.WriteLine(tacka_ab.Opis);
            tacka_ab.Opis = "Ovo je tacka.";
            Console.WriteLine(tacka_ab.Opis);

            /* ----------- Nasledjivanje ---------------- */
            Console.WriteLine("-----------");
            Trougao t = new Trougao { Ime = "pravougli trougao" };
            Cetvrougao cv = new Cetvrougao { Ime = "kvadrat" };
            ispis_imena(t);
            ispis_imena(cv);

            /* --- as operator --- */
            /* Primer je vestacki, ali je potreban kad prenosimo neke parametre u funkciju
             * i kada moze da dodje do ovakve situacije. --- */
            Console.WriteLine("-----------");
            Objekat o = t;
            Objekat o2 = cv;
            // ovo ne moze, greska
            // Console.WriteLine(o.BrojTemena());
            //ali ovako moze:
            Trougao t2 = (Trougao)o;
            Console.WriteLine(t2.BrojTemena());

            //ili jos bolje
            Console.WriteLine((o as Trougao).BrojTemena());
            //Primer koji bi izazvao gresku da ne postoje ovi .? i ?? i as operator.
            //Da je korisceno samo kastovanje kao u gornjem primeru, a ne as operator
            //ne bi moglo da se koristi ?. i ??
            Console.WriteLine((o2 as Trougao)?.BrojTemena() ?? 0);

            /* ---- virtual, override ---- */
            Console.WriteLine("-----------");
            t.Ispis();

            /* ---- Primer za abstract ---- */
            // Prijavljuje gresku ako napisemo
            // Vozilo v = new Vozilo();
            Console.WriteLine("-----------");
            Auto skoda = new Auto();
            skoda.Ispis();

            //Obratiti paznju na pozivanje konstruktora u sledecem primeru

            /* ------- Genericka klasa. --------- */
            Console.WriteLine("-----------");
            var pp = new Podaci<int>();
            var pp2 = new Podaci<decimal>(7.22M); //M mora da se koristi da bi znao da je decimal, a ne float/double
            pp.Parametar = 4;

            pp.Stampaj();
            pp2.Stampaj();

            /* ------- Delegati. --------- */
            Console.WriteLine("-----------");
            Console.WriteLine(Izracunaj(Kvadrat, 3));
            Console.WriteLine(Izracunaj(Kub, 3));
            Console.WriteLine(Izracunaj2(Kvadrat, 3));
            Console.WriteLine(Izracunaj2<double>(Koren, 3));
            Console.WriteLine(Izracunaj3(Kvadrat, 3));
            Console.WriteLine(Izracunaj3<double>(Koren, 3));

            /* ------- Lambda izrazi. --------- */
            Console.WriteLine("-----------");
            //(parameters) => expression-or-statement-block
            //vec smo ih koristili kod funkcija (koje nemaju telo)
            Console.WriteLine(Izracunaj(lambda => lambda * lambda, 3));
            Console.WriteLine(Izracunaj(lambda => lambda * lambda * lambda, 3));
            Console.WriteLine(Izracunaj2(lambda => lambda * lambda, 3));
            Console.WriteLine(Izracunaj2<double>(lambda => Math.Sqrt(lambda), 3));
            Console.WriteLine(Izracunaj3(lambda => lambda * lambda, 3));
            Console.WriteLine(Izracunaj3<double>(lambda => Math.Sqrt(lambda), 3));
            Func<int, int, int> func1 = (lambda1, lambda2) => lambda1 * lambda2;
            Console.WriteLine(func1(3, 5));

            /* ------- Extension method. ---- */
            Console.WriteLine("-----------");

            Covek stud1 = new Covek { Ime = "Pera" };
            stud1.Stampaj();
            stud1.Stampaj2();

            /* ------- Tuples. ---- */
            //korisno kod LINQ
            Console.WriteLine("-----------");

            var par1 = ("Bojan", 23);
            Console.WriteLine(par1.Item1);
            Console.WriteLine(par1.Item2);

            var osoba1 = (Ime: "Danijela", Godina: 33, Pol: 'Z');
            Console.WriteLine(osoba1.Ime);
            Console.WriteLine(osoba1.Godina);
            Console.WriteLine(osoba1.Pol);

            (string ime, int godina) = par1;
            Console.WriteLine(ime);
            Console.WriteLine(godina);

            osoba1 = GetBob();
            Console.WriteLine(osoba1.Ime);
            Console.WriteLine(osoba1.Godina);
            Console.WriteLine(osoba1.Pol);

            /* ------- Char. ---- */
            Console.WriteLine("-----------");
            Console.WriteLine(System.Char.IsLetter('њ'));

            /* ------- Date and Time. ---- */
            Console.WriteLine("-----------");
            DateTime dd = new DateTime(2018, 7, 10);
            Console.WriteLine(dd);
            Console.WriteLine(dd.DayOfWeek);

            Console.WriteLine(DateTime.Now);
            Console.WriteLine(DateTimeOffset.Now);

            /* ------- Parsing: ToString, Parse, CultureInfo. ---- */
            Console.WriteLine("-----------");
            CultureInfo uk = CultureInfo.GetCultureInfo("en-GB");
            CultureInfo us = CultureInfo.GetCultureInfo("en-US");
            CultureInfo sr1 = CultureInfo.GetCultureInfo("sr-Cyrl-RS");
            CultureInfo sr2 = CultureInfo.GetCultureInfo("sr-Latn-RS");

            //"C" oznacava da zelimo valutu
            //"F5" float sa 5 decimale
            //"d" short date, "D" long date, "t" short time
            int broj = 3;
            Console.WriteLine(3.ToString("C"));
            Console.WriteLine(3.ToString("F5"));
            Console.WriteLine($"{broj:C}"); //$ oznacava specijalni string u kome je moguce zapisati promenljive (koje bivaju zamenjene svojim vrednostima)

            Console.WriteLine(3.ToString("C", uk));      // £3.00
            Console.WriteLine(3.ToString("C", us));
            Console.WriteLine(3.ToString("C", sr1));
            Console.WriteLine(3.ToString("C", sr2));

            Console.WriteLine(dd.ToString("d"));
            Console.WriteLine(dd.ToString("D"));
            Console.WriteLine(dd.ToString("t"));

            /* ------- Random. ---- */
            Console.WriteLine("-----------");
            Random r = new Random();
            Console.WriteLine(r.Next(200, 300));
            Console.WriteLine(r.NextDouble());

            //bezbednije, teze za koriscenje
            var rand = System.Security.Cryptography.RandomNumberGenerator.Create();
            byte[] bajt = new byte[32];
            rand.GetBytes(bajt);
            foreach (byte bb in bajt)
                Console.Write(bb + " ");
            Console.WriteLine(BitConverter.ToInt32(bajt, 0));

            //generise string sastavljen od cifara i slova
            Guid g = Guid.NewGuid();
            Console.WriteLine(g.ToString());

            /* ------- Ponovo o nizovima. ---- */
            Console.WriteLine("-----------");
            string[] imena = new string[5] { "Pera", "Vlada", "Nikola", "Janko", "Aca" };
            string ime_nadjeno = Array.Find(imena, n => n.Contains('k'));
            Console.WriteLine(ime_nadjeno);

            string[] imena_nadjena = Array.FindAll(imena, n => n.Contains('k'));
            foreach (string ime1 in imena_nadjena)
                Console.WriteLine(ime1);

            Console.WriteLine("Soriranje 1:");
            Array.Sort(imena); // default ponasanje: sortira rastuce leksikografski
            foreach (string ime1 in imena)
                Console.WriteLine(ime1);

            Console.WriteLine("Soriranje 2:");
            // prema zadatom kriterijumu
            Array.Sort(imena, (im1, im2) => im1.Length == im2.Length ? im1.CompareTo(im2) :
                                                                       im1.Length > im2.Length ? 1 : -1); 
            foreach (string ime1 in imena)
                Console.WriteLine(ime1);

            /* ------- Liste. ---- */
            Console.WriteLine("-----------");
            List<string> lista_niski = new List<string>();
            List<string> lista_niski2 = new List<string>() { "Janko", "Pera", "Ljubisa" };

            for (int i = 0; i < 5; i++)
            {
                string s = Console.ReadLine();
                lista_niski.Add(s);
            }

            // dodavanje vise elementa
            lista_niski.AddRange(new[] { "Jankovic", "Milutinovic" });

            Console.WriteLine("Duzina liste je " + lista_niski.Count);
            lista_niski.RemoveAt(3);
            Console.WriteLine("Nova duzina liste je " + lista_niski.Count);

            lista_niski.RemoveAll(s => Char.IsLower(s[0]));

            foreach (string s in lista_niski)
                Console.WriteLine(s);

            lista_niski2.Insert(2, "Jelena");

            foreach (string s in lista_niski2)
                Console.WriteLine(s);

            /* ------- Dictionary. ---- */
            Console.WriteLine("-----------");
            Dictionary<string, int> artikli = new Dictionary<string, int>();

            artikli["jabuke"] = 30;
            artikli["solja"] = 200;
            artikli["sveska"] = 500;
            artikli["tastatura"] = 2200;

            foreach(string artikl in artikli.Keys)
            {
                Console.WriteLine("Cena artikla " + artikl + " je " + artikli[artikl]);
            }
        }
    }
}
