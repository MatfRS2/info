using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prodavnica.Models;
using Prodavnica.Infrastructure;
using Prodavnica.Models.ViewModels;

namespace Prodavnica.Controllers
{
    public class KorpaController : Controller
    {
        private IProizvodRepozitorijum repozitorijum;

        public KorpaController(IProizvodRepozitorijum repo)
        {
            repozitorijum = repo;
        }

        private Korpa GetKorpa()
        {
            Korpa korpa = HttpContext.Session.GetJson<Korpa>("Korpa") ?? new Korpa();

            return korpa;
        }

        private void SetKorpa(Korpa korpa)
        {
            HttpContext.Session.SetJson("Korpa", korpa);
        }

        public RedirectToActionResult DodajUKorpu(int proizvodId, int kolicina, string url)
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

            //return RedirectToAction("SpisakKorpe");

            //prepravka 3: dodat url
            return RedirectToAction("SpisakKorpe", new { url });
        }

        public ViewResult SpisakKorpe(string url)
        {
            return View(new KorpaViewModel
            {
                Korpa = GetKorpa(),
                Url = url
            });
        }

        public RedirectToActionResult IzbrisiIzKorpe(int proizvodId, string url, int kolicina=1)
        {
            Proizvod proizvod = repozitorijum.Proizvodi
                .Where(p => p.ProizvodId == proizvodId).FirstOrDefault();

            if (proizvod != null)
            {
                Korpa korpa = GetKorpa();
                korpa.ObrisiProizvod(proizvod, kolicina);
                SetKorpa(korpa);
            }

            return RedirectToAction("SpisakKorpe", new { url });
        }
    }
}