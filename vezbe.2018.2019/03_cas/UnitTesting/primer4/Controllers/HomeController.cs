using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using primer4.Models;

namespace primer4.Controllers
{
    public class HomeController : Controller
    {
       /* primer 1 
        public string Index()
        {
            return "Zdravo svete!!";
        }
        */

        private IRepozitorijum repozitorijum;

        /*
        Ovo nije dobro resenje jer ce HomeController svaki put kada bude inicijalizovan da napravi 
        novi repozitorijum (samim tim nece da pamti prethodno unete podatke). Postoji nacin kako ovo da se resi.
        Naime, podatke je moguce pamtiti u bazi. 
        Ipak, za sada, to preskacemo i bavimo se UnitTestovima. 
        Sa IRepozitorijum je napravljena podrska za koriscenje Moq
         */
        public HomeController(IRepozitorijum repo)
        {
            repozitorijum = repo;
        }

        public ViewResult Index() 
        {
            int vreme = DateTime.Now.Hour;

            if (vreme < 12)
                ViewBag.Pozdrav = "Dobro jutro";
            else
                ViewBag.Pozdrav = "Dobar dan";

            return View("MyView");
        }

        [HttpGet]
        public ViewResult FormaZaUnos()
        {
            return View(); /* Vraca stranu FormaZaUnos.cshtml */
        }


        [HttpPost]
        public ViewResult FormaZaUnos(Gosti gost)
        {
            if (ModelState.IsValid) /* Na ovo uticu ogranicenja koja sam stavila u Gosti.cs */
            {
                repozitorijum.dodajGosta(gost);
                return View("Zahvalnica", gost);
            }
            else
                return View(); /* Opet ce prikazati formu, ali ce da pokupi sve probleme koje je vratio ModelState i da ih prikaze. */
        }

        public ViewResult SpisakGostiju()
        {
            return View(repozitorijum.svi_gosti.Where(g => g.DolaziNaZurku == true));
        }

    }
}
