using Microsoft.AspNetCore.Mvc;
using ProdavnicaKozmetike.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace ProdavnicaKozmetike.Controllers{

    [Authorize(Roles = "Manager, Admin")]
    public class AdminController : Controller{

        private IProizvodRepozitory repozitorijum;

        public AdminController(IProizvodRepozitory repo){
            repozitorijum = repo;
        }

        public ViewResult Izmeni(int proizvodID)  =>
            View(repozitorijum.Proizvodi.FirstOrDefault(p => p.ProizvodID == proizvodID));
         
        public ViewResult SpisakProizvoda() =>
            View(repozitorijum.Proizvodi);

        [HttpPost]
        public IActionResult Izmeni(Proizvod proizvod)
        {
            if (ModelState.IsValid)
            {
                repozitorijum.SacuvajProizvod(proizvod);
                TempData["message"] = $"{proizvod.Ime} je sačuvan!";
                return RedirectToAction("SpisakProizvoda");
            }
            else
                return View(proizvod);
            
        }    

        public ViewResult Kreiraj() =>
            View("Izmeni", new Proizvod());

        
        [HttpPost]
        public IActionResult Obrisi(int proizvodID)
        {
            Proizvod izbrisaniProizvod = repozitorijum.BrisiProizvod(proizvodID);

            if (izbrisaniProizvod != null)
            {
                TempData["message"] = $"{izbrisaniProizvod.Ime} je obrisan!!";
            }

            return RedirectToAction("SpisakProizvoda");
        }
          
    }

}