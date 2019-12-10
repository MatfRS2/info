using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc; //potrebno da nasledimo ViewComponent
using Prodavnica.Models;
using Prodavnica.Models.ViewModels;

namespace Prodavnica.Components
{
    public class NavigacijaViewComponent : ViewComponent
    {
        private IProizvodRepozitorijum repozitorijum;

        public NavigacijaViewComponent(IProizvodRepozitorijum repo)
        {
            repozitorijum = repo;
        }

        /* Zelimo da prosledimo kategorije, ali i trenutnu kategoriju.
         * Napravimo klasu koja ima te dve komponente.
         * Prilikom pravljenja NavigacijaViewModel objekta moze se desiti
         * da trenutna kategorija nije postavljena, pa ce 
         * RouteData?.Values["trenutnaKategorija"] biti null.
         * Zbog toga koristimo ?. (elvis operator) prilikom kastovanja u string.
         */
        public IViewComponentResult Invoke()
        {
            return View(
                new NavigacijaViewModel
                {
                    Kategorije = repozitorijum.Proizvodi
                        .Select(p => p.Kategorija)
                        .Distinct()
                        .OrderBy(x => x),
                    TrenutnaKategorija = RouteData?.Values["trenutnaKategorija"]?.ToString()
                });
        }
    }
}
