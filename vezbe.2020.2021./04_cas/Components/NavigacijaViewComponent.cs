using Microsoft.AspNetCore.Mvc;
using Prodavnica2.Models;
using Prodavnica2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodavnica2.Components
{
    public class NavigacijaViewComponent : ViewComponent
    {
        private IProizvodRepozitorijum repozitorijum;

        public NavigacijaViewComponent(IProizvodRepozitorijum repo)
        {
            repozitorijum = repo;
        }

        public IViewComponentResult Invoke() => View(
            new NavigacijaViewModel
            {
                Kategorije = repozitorijum.Proizvodi
                            .Select(p => p.Kategorija)
                            .Distinct()
                            .OrderBy(k => k),

                TrenutnaKategorija = RouteData?.Values["kategorija"]?.ToString()
            });
        
    }
}
