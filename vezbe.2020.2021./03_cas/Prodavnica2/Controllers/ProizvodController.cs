using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prodavnica2.Models;

namespace Prodavnica2.Controllers
{
    public class ProizvodController : Controller
    {
        private IProizvodRepozitorijum repozitorijum;

        public ProizvodController(IProizvodRepozitorijum repo)
        {
            repozitorijum = repo;
        }

        public ViewResult Spisak()
        {
            return View(repozitorijum.Proizvodi);
        }

        public ViewResult Spisak2()
        {
            var proizvodi = repozitorijum.Proizvodi.Where(p => p.Cena > 25).ToList<Proizvod>();
            ViewBag.BrojProizvoda = proizvodi.Count();

            return View(proizvodi);
        }
    }
}
