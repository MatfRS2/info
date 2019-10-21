using Microsoft.AspNetCore.Mvc;
using ProdavnicaKozmetike.Models;
using System.Collections.Generic;
using System.Linq;
using ProdavnicaKozmetike.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace ProdavnicaKozmetike.Controllers
{
    public class PorudzbinaController : Controller
    {
        private IPorudzbineRepozitorijum repozitorijum;

        public PorudzbinaController(IPorudzbineRepozitorijum repo)
        {
            repozitorijum = repo;
        }
        
        private Korpa GetKorpa(){
            Korpa korpa = HttpContext.Session.GetJson<Korpa>("Korpa") ?? new Korpa();
            return korpa;
        }

        private void SetKorpa(Korpa korpa){
            HttpContext.Session.SetJson("Korpa", korpa);
        }

        [Authorize]
        public ViewResult SpisakNeposlatihPorudzbina()
        {
            return View(repozitorijum.Porudzbine.Where(p => p.Poslato == false));
        }

        [HttpPost]
        [Authorize]
        public IActionResult OznaciKaoPoslato(int porudzbinaID)
        {
            Porudzbina porudzbina = repozitorijum.Porudzbine.FirstOrDefault(p => p.PorudzbinaID == porudzbinaID);

            if (porudzbina != null)
            {
                porudzbina.Poslato = true;
                repozitorijum.SacuvajPorudzbinu(porudzbina);
            }
            
            return RedirectToAction("SpisakNeposlatihPorudzbina");
        }

        [HttpGet]
        public ViewResult Placanje()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Placanje(Porudzbina porudzbina)
        {
             Korpa korpa = GetKorpa();

            if (korpa.listaProizvodaUKorpi.Count() == 0){
                ModelState.AddModelError("", "Vaša korpa je prazna!");
            }

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
            Korpa korpa = GetKorpa();
            korpa.ObrisiKorpu();
            SetKorpa(korpa);

            return View();
        }
    }    
}