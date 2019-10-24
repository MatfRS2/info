using System;
using System.Linq;
using System.Collections.Generic;
using linqIzrazi;

namespace linqPrimeri
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            List<Osoba> osobe = new List<Osoba>();

            osobe.Add( new Osoba {Ime = "Danijela", Pol = "zenski", GodinaRodjenja = 1986} );
            osobe.Add(new Osoba { Ime = "Pera", Pol = "muski", GodinaRodjenja = 1990 });
            osobe.Add(new Osoba { Ime = "Joca", Pol = "muski", GodinaRodjenja = 1987 });
            osobe.Add(new Osoba { Ime = "Sava", Pol = "muski", GodinaRodjenja = 1975 });
            osobe.Add(new Osoba { Ime = "Raka", Pol = "muski", GodinaRodjenja = 1993 });
            osobe.Add(new Osoba { Ime = "Ana", Pol = "zenski", GodinaRodjenja = 1990 });
            osobe.Add(new Osoba { Ime = "Marija", Pol = "zenski", GodinaRodjenja = 1984 });
            osobe.Add(new Osoba { Ime = "Ivana", Pol = "zenski", GodinaRodjenja = 1974 });
            osobe.Add(new Osoba { Ime = "Sanja", Pol = "zenski", GodinaRodjenja = 1992 });

            // LINQ izrazi (opis linija u onom redu u kome se pojavljuju):
            //
            // from (p) in c -- ova linija znaci da se uzimaju elementi iz kolekcije c (tekuci element oznacen sa (p)). 
            //                Moze biti vise uzastopnih from linija, sto igra ulogu Dekartovog proizvoda.
            // where uslov  -- opciona linija, moze sadrzati proizvoljan C# logicki izraz. U njemu tipicno 
            //                 ucestvuju promenljive uvedene u from liniji.
            //
            // orderby vrednost [ascending|descending] -- sortira kolekciju po vrednosti, opadajuce ili rastuce.
            //                                            ukoliko zelimo dodatno sortiranje po drugoj vrednosti,
            //                                            mozemo vise vrednosti razdvojiti zarezima. Podrazumeva 
            //                                            se rastuce sortiranje.
            //
            // select vrednost -- uvek dolazi na kraju, bira sta zelimo selektovati.
            // 
            // group p by vrednost -- pri cemu se vrednost izracunava na osnovu p, gde je p varijabla uvedena
            //                        u okviru from linije. Vrednost je najcesce neko svojstvo (atribut) u p,
            //                        po kome vrsimo grupisanje. Svaki LINQ izraz se zavrsava TACNO JEDNOM od
            //                        select ili group by linija (dakle uvek stoji jedna ili druga na kraju, ali
            //                        nikako obe ne mogu biti u izrazu). group by vraca kolekciju kolekcija, pri
            //                        cemu svaka od tih kolekcija ima svojstvo Key (vrednost po kome je grupisana)
            //                        a sastoji se od svih elemenata originalne strukture koji imaju tu vrednost
            //                        datog svojstva.
            //
            // Spajanje:
            //
            // from p1 in c1
            // join p2 in c2
            // on p1.S1 equals p2.S2 -- ovim se dobija spajanje dve tabele po svojstvima S1 iz prve i S2 iz druge.
            //                          Ovo se moze postici i sa dve from linije, pa restrikcijom, ali je ovako
            //                          efikasnije.
            //
            // Pored ovih, mogu postojati jos i linije:
            //
            // let p = izraz         -- ovim se uvodi ime za neki izraz, koje se u daljem upitu moze koristiti
            //                          umesto tog izraza. Ova linija tipicno ide posle from linije, a pre uslova, 
            //                          ili izmedju visestrukih from linija (npr. za definisanje pomocnih tabela).
            //
            // into var              -- ova linija ide iza select ili group by i u nju se smesta rezultat upita,
            //                          kako bi se na ovaj upit mogao nadovezati drugi upit koji koristi rezultat
            //                          prethodnog kao tabelu koju pretrazuje. Ovo je ekvivalent SQL-ovoj WITH-AS
            //                          konstrukciji.

            // Izdvaja sve muskarce
            var muskarci = from p in osobe
                           where p.Pol == "muski"
                           select p;

            foreach (Osoba p in muskarci)
            {
                Console.WriteLine(p.ToString());
            }

             Console.WriteLine("** II **");
            //II nacin
            var muskarci2 = osobe.Where(p => p.Pol == "muski");

            foreach (Osoba p in muskarci2)
            {
                Console.WriteLine(p.ToString());
            }

            // Izdvaja sve devojke, selektuje samo imena
            Console.WriteLine("---------------------------------------------------------------------------");

            var imena_devojke = from p in osobe
                                where p.Pol == "zenski"
                                select p.Ime;
            foreach (string p in imena_devojke)
            {
                Console.WriteLine(p);
            }

            Console.WriteLine("** II **");
            //II nacin
            var imena_devojke2 = osobe.Where(p => p.Pol == "zenski").Select(p => p.Ime); /* Select ide na kraj */
            foreach (string p in imena_devojke)
            {
                Console.WriteLine(p);
            }

            // Izdvaja sve osobe starije od 18 godina, selektuje ime i godinu rodjenja. Sortira po godini rodjenja rastuce, a zatim po imenima, rastuce.
            Console.WriteLine("---------------------------------------------------------------------------");
            var punoletni = from p in osobe
                            where DateTime.Now.Year > p.GodinaRodjenja + 18
                            orderby p.GodinaRodjenja ascending, p.Ime descending
                            select new {p.Ime, p.GodinaRodjenja};
            foreach (var p in punoletni)
            {
                Console.WriteLine(p);
            }

            Console.WriteLine("** II **");
            //II nacin
            var punoletni2 = osobe.Where(p => DateTime.Now.Year > p.GodinaRodjenja + 18).OrderBy(p => p.GodinaRodjenja).ThenByDescending(p => p.Ime).Select(p => new {p.Ime, p.GodinaRodjenja});

            foreach (var p in punoletni2)
            {
                Console.WriteLine(p);
            }


            // Izdvaja kolekciju grupa, pri cemu se grupisanje vrsi po polu.
            Console.WriteLine("---------------------------------------------------------------------------");
            var grupa = from p in osobe
                        group p by p.Pol;
        
            // Za svaku grupu, prikazujemo kljuc grupisanja, a zatim i clanove grupe.
            foreach (var g in grupa)
            {
                Console.WriteLine("Pol: " + g.Key);
                foreach (var elem in g)
                    Console.WriteLine(elem);
            }

            Console.WriteLine("** II **");
            //II nacin
            var grupa2 = osobe.GroupBy(p => p.Pol);

            foreach (var g in grupa2)
            {
                Console.WriteLine("Pol: " + g.Key);
                foreach (var elem in g)
                    Console.WriteLine(elem);
            }


            // Izdvajamo statistike o grupi: broj elemenata grupe, prosecnu vrednost godina clanova grupe.
            Console.WriteLine("---------------------------------------------------------------------------");
            var statistika = from p in grupa
                             select new {Pol = p.Key, Broj_el = p.Count(), Prosek = p.Average(y => DateTime.Now.Year - y.GodinaRodjenja)};
            foreach (var s in statistika)
            {
                Console.WriteLine(s.Pol + " " + s.Broj_el + " " + s.Prosek);
            }


            Console.WriteLine("** II **");
            //II nacin
            var statistika2 = osobe.GroupBy(p => p.Pol).Select(p => new {Pol = p.Key, 
                                        Broj_el = p.Count(), Prosek = p.Average(g => DateTime.Now.Year - g.GodinaRodjenja)});

            foreach (var s in statistika2)
            {
                Console.WriteLine(s.Pol + " " + s.Broj_el + " " + s.Prosek);
            }


            //Izdvojiti sve parove muskaraca i zena rodjenih iste godine
            Console.WriteLine("---------------------------------------------------------------------------");
            var parovi = from o1 in (from p in osobe where p.Pol == "muski" select p)
                         join o2 in (from p in osobe where p.Pol == "zenski" select p)
                         on o1.GodinaRodjenja equals o2.GodinaRodjenja
                         select new {Musko = o1.Ime, Zensko = o2.Ime, GodinaRodjenja = o1.GodinaRodjenja};

            foreach (var p in parovi)
                Console.WriteLine(p);

            Console.WriteLine("** II **");
            //II nacin -- komplikovano, za domaci :)
        }
    }
}
