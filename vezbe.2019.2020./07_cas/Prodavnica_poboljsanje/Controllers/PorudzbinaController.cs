using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prodavnica.Models;

namespace Prodavnica.Controllers
{
    public class PorudzbinaController : Controller
    {
        private IPorudzbineRepozitorijum repozitorijum;
        Korpa korpa;

        public PorudzbinaController(IPorudzbineRepozitorijum repo, Korpa korpaIzServisa)
        {
            repozitorijum = repo;
            korpa = korpaIzServisa;
        }

        [HttpGet]
        public ViewResult  Placanje()
        {
            return View();
        }

        /* Stavlja se IActionResult jer moze biti vise razlicitih izlaza
         * u zavisnosti od uspesnosti porudzbine.
         */
        [HttpPost]
        public IActionResult Placanje(Porudzbina porudzbina)
        {
            if (korpa.ListaProizvodaUKorpi.Count() == 0)
            {
                ModelState.AddModelError("", "Vasa korpa je prazna");
            }

            if (ModelState.IsValid)
            {
                porudzbina.ListaProizvodaUKorpi = korpa.ListaProizvodaUKorpi;
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