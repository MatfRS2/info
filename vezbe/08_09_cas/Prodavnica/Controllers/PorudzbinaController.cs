using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prodavnica.Models;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize (Roles = "ObicanKorisnik")]
        public ViewResult SpisakNeposlatihPorudzbina()
        {
            return View(repozitorijum.Porudzbine.Where(p => p.Poslato == false));
        }

        [Authorize (Roles = "ObicanKorisnik")]
        [HttpPost]
        public RedirectToActionResult OznaciKaoPoslato(int porudzbinaID)
        {
            Porudzbina porudzbina = repozitorijum.Porudzbine
                .FirstOrDefault(p => p.PorudzbinaId == porudzbinaID);

            if (porudzbina != null)
            {
                porudzbina.Poslato = true;
                repozitorijum.SacuvajPorudzbinu(porudzbina);
            }

            return RedirectToAction("SpisakNeposlatihPorudzbina");
        }
    }
}