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
        private Korpa korpa;

        public KorpaViewComponent(Korpa korpaIzServisa)
        {
            korpa = korpaIzServisa;
        }        

        public IViewComponentResult Invoke()
        {
            return View(korpa);
        }
    }
}
