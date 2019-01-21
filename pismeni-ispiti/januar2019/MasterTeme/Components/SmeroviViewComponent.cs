using Microsoft.AspNetCore.Mvc;
using MasterTeme.Models;
using System.Linq;
using System.Collections.Generic;

namespace MasterTeme.Components{

    public class SmeroviViewComponent : ViewComponent {
        private ApplicationDbContext repozitorijum;

        public SmeroviViewComponent(ApplicationDbContext repo)
        {
            repozitorijum = repo;
        }

        public IViewComponentResult Invoke() =>
            View(repozitorijum.Studenti.Select(p => p.Smer).Distinct().OrderBy(p => p));
        
    }
}