using Microsoft.AspNetCore.Mvc;
using ProdavnicaTehnike.Models;
using System.Linq;

namespace ProdavnicaTehnike.Controllers 
{
    public class HomeController : Controller 
    {
        private DataContext repozitorijum;

        public HomeController(DataContext repo)
        {
            repozitorijum = repo;
        }

        public IActionResult Index() {
            ViewBag.Naslov = "Prodavnica Tehnike";
            return View(repozitorijum.Proizvodi.First());
        }
    }
}