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
        private Korpa korpa;

        public KorpaController(IProizvodRepozitorijum repo, Korpa korpaIzServisa)
        {
            repozitorijum = repo;
            korpa = korpaIzServisa;
        }

        public RedirectToActionResult DodajUKorpu(int proizvodId, int kolicina, string url)
        {
            Proizvod proizvod = repozitorijum.Proizvodi
               .Where(p => p.ProizvodId == proizvodId).FirstOrDefault();

            if (proizvod != null)
                korpa.DodajProizvod(proizvod, kolicina);
           

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
                Korpa = korpa,
                Url = url
            });
        }

        public RedirectToActionResult IzbrisiIzKorpe(int proizvodId, string url, int kolicina=1)
        {
            Proizvod proizvod = repozitorijum.Proizvodi
                .Where(p => p.ProizvodId == proizvodId).FirstOrDefault();

            if (proizvod != null)
                korpa.ObrisiProizvod(proizvod, kolicina);

            return RedirectToAction("SpisakKorpe", new { url });
        }
    }
}