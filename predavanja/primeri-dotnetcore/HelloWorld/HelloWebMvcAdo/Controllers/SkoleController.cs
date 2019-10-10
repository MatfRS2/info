using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelloWebMvcAdo.Controllers
{
    public class SkoleController : Controller
    {
        [Route("Skole")]
        [Route("Skole/Index")]
        public IActionResult Index()
        {
            ProbaContext context = HttpContext.RequestServices.GetService(typeof(ProbaContext)) as ProbaContext;

            return View(context.VratiSveSkole());
        }
    }
}