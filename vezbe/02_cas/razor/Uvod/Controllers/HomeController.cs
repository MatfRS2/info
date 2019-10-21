using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Uvod.Models;

namespace Uvod.Controllers
{
    /*
     * Nazivi klasa su bitni. Svaki kontroler se naziva sa ImeController.
     * Kontroler "kotrolise" sve sto je u Views/Ime/ folderu.
     * Znaci, HomeController "kontrolise" sve sto je u
     * Views/Home/ folderu.
     * U opstem slucaju, metodi odgovaraju Razor stranicama.
     * Index metod ce da odgovara Index.cshtml Razor stranici.
     * Konvencija se moze menjati u konfiguraciji ili prilikom rutiranja.
     */
    public class HomeController : Controller
    {
        /* Dobar tekst o povratim vrednostima metoda 
         * u kontroleru:
         * https://stackoverflow.com/questions/4743741/difference-between-viewresult-and-actionresult
         */
        public ViewResult Index()
        {
            Proizvod proizvodMis = new Proizvod
            {
                ProizvodID = 1,
                Cena = 12M,
                Ime = "mis",
                Kategorija = "periferni uredjaji",
                Opis = "laserski sa kablom"
            };

            //primer kako se izmedju kontrolera i pogleda mogu prenositi podaci
            //koriscnjenjem ViewBag-a
            //Ipak, u opstem slucaju ViewBag se ne koristi za ovakve namene
            ViewBag.Kolicina = 2;

            return View(proizvodMis);
        }

        public ViewResult Index1()
        {
            Proizvod[] proizvodi =
            {
                new Proizvod {Ime = "tastatura", Cena = 200M},
                new Proizvod {Ime = "laptop", Cena = 500M},
                new Proizvod {Ime = "monitor", Cena = 300M}
            };

            return View(proizvodi);
        }
    }
}