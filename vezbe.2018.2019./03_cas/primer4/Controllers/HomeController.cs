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
        GostiDataAccessLayer objgost = new GostiDataAccessLayer();

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
            if (ModelState.IsValid) 
            {
                objgost.dodajGosta(gost);
                return View("Zahvalnica", gost);
            }
            else
                return View(); 
        }

        public ViewResult SpisakGostiju()
        {
            List<Gosti> gosti = new List<Gosti>();  
            gosti = objgost.sviGosti().ToList();

            return View(gosti.Where(g => g.DolaziNaZurku == true));
        }

    }
}
