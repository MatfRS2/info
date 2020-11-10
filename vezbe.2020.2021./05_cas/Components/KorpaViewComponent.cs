using Microsoft.AspNetCore.Mvc;
using Prodavnica2.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prodavnica2.Models;

namespace Prodavnica2.Components
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
