using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prodavnica.Models;
using Prodavnica.Infrastructure;

namespace Prodavnica.Controllers
{
    public class KorpaController : Controller
    {
        private IProizvodRepozitorijum repozitorijum;

        public KorpaController(IProizvodRepozitorijum repo)
        {
            repozitorijum = repo;
        }

        /* Cita podatke iz Sesije pod kljucem "Korpa". 
         * Ukoliko pod kljucem "Korpa" u sesiji ne postoji nista (null), onda se kreira nova korpa.
        */
        private Korpa GetKorpa()
        {
            Korpa korpa = HttpContext.Session.GetJson<Korpa>("Korpa") ?? new Korpa();

            return korpa;
        }

        /* Cuva podatke u sesije pod kljucem "Korpa"
        */
        private void SetKorpa(Korpa korpa)
        {
            HttpContext.Session.SetJson("Korpa", korpa);
        }

        public RedirectToActionResult DodajUKorpu(int proizvodId, int kolicina)
        {
            Proizvod proizvod = repozitorijum.Proizvodi
               .Where(p => p.ProizvodId == proizvodId).FirstOrDefault();

            if (proizvod != null)
            {
                Korpa korpa = GetKorpa();
                korpa.DodajProizvod(proizvod, kolicina);
                SetKorpa(korpa);
            }

            //probati prvo ovo
            //return RedirectToAction("Spisak", "Proizvod");

            return RedirectToAction("SpisakKorpe");
        }

        public ViewResult SpisakKorpe()
        {
            return View(GetKorpa());
        }
    }
}