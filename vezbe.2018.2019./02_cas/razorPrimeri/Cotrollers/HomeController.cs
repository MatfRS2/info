using Microsoft.AspNetCore.Mvc;
using razorPrimeri.Models;

namespace razorPrimeri.Controllers{

    public class HomeController : Controller {

        public ViewResult Index()
        {
            Proizvodi p = new Proizvodi {
                ProizvodID = 1,
                Ime = "peskir",
                Opis = "veliki peskir za plazu",
                Kategorija = "oprema za letovanje",
                Cena = 2000M
            };

            //2.
            ViewBag.naLageru = 2;

            return View(p);
        }


        public ViewResult Iteracija()
        {
            Proizvodi[] niz_proizvoda = {
                new Proizvodi { ProizvodID = 1, Ime = "peskir", Opis = "veliki peskir za plazu", 
                                Kategorija = "oprema za letovanje", Cena = 2000M},
                new Proizvodi { ProizvodID = 2, Ime = "papuce", Opis = "japanke", 
                                Kategorija = "oprema za letovanje", Cena = 500M},
                new Proizvodi { ProizvodID = 3, Ime = "suncobran", Opis = "crveni veliki suncobran", 
                                Kategorija = "oprema za letovanje", Cena = 1500M},
                new Proizvodi { ProizvodID = 4, Ime = "pegla", Opis = "pegla na paru", 
                                Kategorija = "mali kucni aparati", Cena = 6500M},
                new Proizvodi { ProizvodID = 5, Ime = "blender", Opis = "stakleni blender", 
                                Kategorija = "mali kucni aparati", Cena = 4500M},           
            };

            return View(niz_proizvoda);
        }

    }

}