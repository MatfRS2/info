using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; /* da bi mogla da koristim liste */
using csharpPrimeri.Models;
using System.Linq; /* Da bi mogao select da radi */

/* za asihron metod */
using System.Net.Http;
using System.Threading.Tasks;

namespace csharpPrimeri.Controllers {

    public class HomeController : Controller {

        public ViewResult Index()
        {
            //1. jednostavan ispis
            /* return View ( new string [] {"osobine", "C#", "jezika"} ); */

            //2. null conditional operator (operator ?? dodati na kraju primera da bi se videla razlika)
            List<string> rezultat = new List<string>();

            foreach (Proizvod p in Proizvod.nizProizvoda())
            {
                string ime = p?.Ime ?? "<nema>";
                decimal? cena = p?.Cena ?? 0;

                //2.1.
                string povezan = p?.Povezani?.Ime ?? "<nema>";

                //3.
                string kategorija = p?.Kategorija ?? "<nema>";

                //3.1.
                bool naLageru = p?.NaLageru ?? false;

                //2.
                //rezultat.Add(string.Format("Ime: {0}, Cena: {1}", ime, cena));

                //2.1.
                //rezultat.Add ( string.Format ("Ime: {0}, Cena: {1}, Povezan: {2}, Kategorija: {3}", ime, cena, povezan, kategorija));

                //4.
                rezultat.Add($"Ime: {ime}, Cena: {cena:C2}, Povezan : {povezan}, Kategorija : {kategorija}, NaLageru : {naLageru}");
            }

            //5.
            /*
            Dictionary<string, Proizvod> proizvodi = new Dictionary<string, Proizvod> {
                ["sveska"] = new Proizvod { Ime = "sveska", Cena = 275M },
                ["olovka"] = new Proizvod { Ime = "Olovka", Cena = 48.95M }
            };
            return View("Index", proizvodi.Keys);
            */

            return View (rezultat);
        }

        //6. pattern matching
        public ViewResult Strana2()
        {
            object[] podaci = new object [] {"pera", 12.3M, 145, -12, "jagoda", 45M, 10};

            decimal zbir = 0;

            for (int i = 0; i<podaci.Length; i++)
            {
                if (podaci[i] is decimal broj) /* obratiti paznju na 'is' */
                    zbir += broj;
            }

            decimal zbir2 = 0;
            int zbir_celih = 0;

            for (int i = 0; i < podaci.Length; i++)
            {
                switch (podaci[i])
                {
                    case int vrednost: zbir_celih += vrednost; break;
                    case decimal broj when broj > 30M:
                            zbir2 += broj;
                            break;
                }
            }


            return View(new string[] {$"Zbir : {zbir:C2}", $"zbir2 : {zbir2}", $"Zbir celih : {zbir_celih}"});

        }

        //7.2. Selektori za lambda izraze
        bool filtrirajPoCeni_primer_za_lambda(Proizvod p)
        {
            return ((p?.Cena ?? 0) >= 20);
        }

        bool filtrirajPoImenu_primer_za_lambda(Proizvod p)
        {
            return (p?.Ime?[0] == 'k');
        }

        //7.
        public ViewResult Strana3()
        {
            KorpaZaKupovinu korpa = new KorpaZaKupovinu { Proizvodi = Proizvod.nizProizvoda() };
            List<string> rezultat = new List<string>();
            
            /* 7.1.
            decimal cena = korpa.ukupnaCena();
            return View( new string [] {$"Cena: {cena:C2}"});
            */
            /* 7.2.
            foreach (Proizvod p in korpa.filtrirajPoCeni(50))
            {
                string ime = p?.Ime ?? "<none>";

                rezultat.Add($"Ime : {ime}");
            }

            foreach (Proizvod p in korpa.filtrirajPoImenu('k'))
            {
                string ime = p?.Ime ?? "<none>";

                rezultat.Add($"Ime : {ime}");
            }
            */

            /* 7.3. -- koriscenjem lambda izraza */
            foreach (Proizvod p in korpa.filtriraj(filtrirajPoCeni_primer_za_lambda))
            {
                string ime = p?.Ime ?? "<none>";

                rezultat.Add($"Ime : {ime}");
            }

            return View(rezultat);
        }


        //7.4.
        /*
        public ViewResult Strana4()
        {
            return View(Proizvod.nizProizvoda().Select(p => p?.Ime));
        }
        */
        // jos jednostavnije
        public ViewResult Strana4() => View(Proizvod.nizProizvoda().Select(p => p?.Ime));



        //8. -- anonimni tipovi
        public ViewResult Strana5()
        {
            var biblioteka = new [] { 
                new { Naslov = "Rat i mir", Autor = "Lav Tolstoj"},
                new { Naslov = "Prokleta avlija", Autor = "Ivo Andric"}
            };

            return View(biblioteka.Select( p => p.GetType().Name));
        }

        //9. -- asihrone funkcije
        public async Task<ViewResult> Strana6()
        {
             HttpClient client = new HttpClient();  

            // GetStringAsync returns a Task<string>. That means that when you await the  
            // task you'll get a string (urlContents).  
            Task<string> getStringTask = client.GetStringAsync("http://msdn.microsoft.com");  

            // You can do work here that doesn't rely on the string from GetStringAsync.  
            //DoIndependentWork();  

            // The await operator suspends AccessTheWebAsync.  
            //  - AccessTheWebAsync can't continue until getStringTask is complete.  
            //  - Meanwhile, control returns to the caller of AccessTheWebAsync.  
            //  - Control resumes here when getStringTask is complete.   
            //  - The await operator then retrieves the string result from getStringTask.  
            string urlContents = await getStringTask;  

            // The return statement specifies an integer result.  
            // Any methods that are awaiting AccessTheWebAsync retrieve the length value.  
            return View(new string [] { $"{urlContents.Length}"}); 
        }
 
    }
    

}