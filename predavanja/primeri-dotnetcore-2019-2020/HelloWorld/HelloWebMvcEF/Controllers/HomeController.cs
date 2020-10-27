using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HelloWebMvcEF.Models;

namespace HelloWebMvcEF.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("Opis")]
        [Route("Home/Opis")]
        public IActionResult Opis()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Route("Kontakt")]
        [Route("Home/Kontakt")]
        public IActionResult Kontakt()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
