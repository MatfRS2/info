using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prodavnica2.Models;

namespace Prodavnica2.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            Proizvod proizvodMis = new Proizvod
            {
                ProizvodID = 1,
                Cena = 12M,
                Ime = "mis",
                Kategorija = "periferni uredjaj",
                Opis = "laserski sa kablom"
            };

            ViewBag.Kolicina = 2;
            return View(proizvodMis);
        }

        public ViewResult Index2()
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
