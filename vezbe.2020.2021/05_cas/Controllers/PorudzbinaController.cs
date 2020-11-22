using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prodavnica2.Models;

namespace Prodavnica2.Controllers
{
    public class PorudzbinaController : Controller
    {
        private IPorudzbineRepozitorijum repozitorijum;
        private Korpa korpa;

        public PorudzbinaController(IPorudzbineRepozitorijum  repo, Korpa korpaIzServisa)
        {
            repozitorijum = repo;
            korpa = korpaIzServisa;
        }

        [HttpGet]
        public ViewResult Placanje()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Placanje(Porudzbina porudzbina)
        {
            if (korpa.listaProizvodaUKorpi.Count() == 0)
                ModelState.AddModelError("", "Vasa korpa je prazna");

            if (ModelState.IsValid)
            {
                porudzbina.listaProizvodaUKorpi = korpa.listaProizvodaUKorpi;
                repozitorijum.SacuvajPorudzbinu(porudzbina);
                return RedirectToAction("Zahvalnica");
            }
            else
                return View();
        }

        public ViewResult Zahvalnica()
        {
            korpa.ObrisiKorpu();

            return View();
        }
    }
}
