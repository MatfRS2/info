using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prodavnica.Models;

namespace Prodavnica.Controllers
{
    /* Ime kontrolera se uvek zavrsava sa Controller. */
    /* MVC kreira instancu ProzivodControllera svaki put prilikom obrate 
     * HTTP zahteva (koji se odnose na ovaj kontroler).
     * Tom prilikom, na osnovu konstruktora i onoga sto je napisano u
     * Starup klasi nova instanca Laznog repozitorijuma se kreira i koristi.
     * 
     * ProizvodController kontrolise sve u Views/Proizvod folderu (znaci imena su bitna!!!)
     * 
     * Metod Spisak() kontrolise Views/Proizvod/Spisak.cshtml (znaci imena su bitna!!!)
     */
    public class ProizvodController : Controller
    {
        private IProizvodRepozitorijum repozitorijum;

        public ProizvodController(IProizvodRepozitorijum repo)
        {
            repozitorijum = repo;
        }

        public ViewResult Spisak() =>
            View(repozitorijum.Proizvodi);

        /* Kako se korisiti IQueryable za Proizvode treba biti pazljiv.
         * IQueryable ima puno prednosti,, ali i manu da moze da generise mnogo upita.
         * Primer dole: ako se ne stavi ToList, resenje ce da radi
         * ali ce da stvori jedan upit za ViewBag i onda jos jedan za return View(repozitorijum)
         * To je 2 x obracanje bazi sto moze biti skupo.
         * Zato stavimo ToList da bih odmah izracunao upit i onda za 
         * ViewBag se ne obraca ponovo bazi.
         */
        public ViewResult Spisak2()
        {
            var proizvodi = repozitorijum.Proizvodi.Where(p => p.Cena > 25).ToList<Proizvod>();
            ViewBag.ProductCount = proizvodi.Count();

            return View(proizvodi);
        }

    }
}