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
                Repozitorijum.dodajGosta(gost);
                return View("Zahvalnica", gost);
            }
            else
                return View(); /* Opet ce prikazati formu, ali ce da pokupi sve probleme koje je vratio ModelState i da ih prikaze. */
        }

        public ViewResult SpisakGostiju()
        {
            return View(Repozitorijum.svi_gosti.Where(g => g.DolaziNaZurku == true));
        }

    }
}
