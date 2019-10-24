using ProdavnicaKozmetike.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ProdavnicaKozmetike.Infrastructure;
using ProdavnicaKozmetike.Models.ViewModels;

namespace ProdavnicaKozmetike.Controllers{

    public class KorpaController : Controller {

        private IProizvodRepozitory repozitorijum;

        public KorpaController(IProizvodRepozitory repo)
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

        public RedirectToActionResult DodajUKorpu(int proizvodID, string Url)
        {
            Proizvod proiz = repozitorijum.Proizvodi.Where(p => p.ProizvodID == proizvodID).FirstOrDefault();

            if (proiz != null)
            {
                Korpa korpa = GetKorpa();
                korpa.DodajProizvod(proiz, 1);
                SetKorpa(korpa);
            }

            // return RedirectToAction("SpisakProizvoda", "Proizvod");

            return RedirectToAction("SpisakKorpe", new { Url });
        }

        public RedirectToActionResult IzbrisiIzKorpe(int proizvodID, string Url)
        {
            Proizvod proiz = repozitorijum.Proizvodi.Where( p => p.ProizvodID == proizvodID).FirstOrDefault();

            if (proiz != null)
            {
                Korpa korpa = GetKorpa();
                korpa.ObrisiProizvod(proiz);
                SetKorpa(korpa);
            }

            return RedirectToAction("SpisakKorpe", new { Url });
        }

        public ViewResult SpisakKorpe(string Url)
        {
            //return View(GetKorpa());

            return View(new KorpaViewModel{
                Korpa = GetKorpa(),
                Url = Url
            });
        }

    }

}