using Microsoft.AspNetCore.Mvc;
using ProdavnicaKozmetike.Models;
using System.Linq;
using System.Collections.Generic;

namespace ProdavnicaKozmetike.Components{

    public class NavigacijaViewComponent : ViewComponent {
        private IProizvodRepozitory repozitorijum;


        /* I varijanta
        public string Invoke()
        {
            return "Zdravo svima iz komponente!";
        }
        */
        public NavigacijaViewComponent(IProizvodRepozitory repo)
        {
            repozitorijum = repo;
        }

        public IViewComponentResult Invoke()
        {
            //return View(
                //repozitorijum.Proizvodi.Select(p => p.Kategorija).Distinct().OrderBy(p => p));

            //8.
            @ViewBag.TrenutnaKategorija = RouteData?.Values["kategorija"];

            List<string> kategorije = repozitorijum.Proizvodi.Select(p => p.Kategorija).Distinct().OrderBy(p => p).ToList();

            for(int i=0; i<kategorije.Count; i++)
               kategorije[i] = char.ToUpper(kategorije[i].First()) + kategorije[i].Substring(1);

            return View(kategorije);
        }
    }
}