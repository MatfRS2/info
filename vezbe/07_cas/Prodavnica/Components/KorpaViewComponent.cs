using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prodavnica.Models;
using Prodavnica.Infrastructure;

namespace Prodavnica.Components
{
    public class KorpaViewComponent : ViewComponent
    {
        private Korpa GetKorpa()
        {
            Korpa korpa = HttpContext.Session.GetJson<Korpa>("Korpa") ?? new Korpa();
            return korpa;
        }


        public IViewComponentResult Invoke()
        {
            Korpa korpa = GetKorpa();
            return View(korpa);
        }
    }
}
