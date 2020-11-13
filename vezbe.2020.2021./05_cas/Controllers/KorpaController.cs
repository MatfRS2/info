using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prodavnica2.Models;
using Prodavnica2.Infrastructure;
using Prodavnica2.Models.ViewModels;

namespace Prodavnica2.Controllers
{
    public class KorpaController : Controller
    {
        private IProizvodRepozitorijum repozitorijum;
        private Korpa korpa;

        public KorpaController(IProizvodRepozitorijum repo, 
                               Korpa korpaIzServisa)
        {
            repozitorijum = repo;
            korpa = korpaIzServisa;
        }

        public RedirectToActionResult DodajUKorpu(int proizvodId, string url, int kolicina = 1)
        {
            Proizvod proizvod = repozitorijum.Proizvodi
                .Where(p => p.ProizvodID == proizvodId).FirstOrDefault();

            if (proizvod != null)
                korpa.DodajProizvod(proizvod, kolicina);

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

        public RedirectToActionResult IzbrisiIzKorpe(int proizvodId, string url)
        {
            Proizvod proizvod = repozitorijum.Proizvodi
                .Where(p => p.ProizvodID == proizvodId).FirstOrDefault();

            if (proizvod != null)
                korpa.ObrisiProizvod(proizvod);

            return RedirectToAction("SpisakKorpe", new { url });
        }
    }
}
