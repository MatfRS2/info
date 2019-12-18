using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prodavnica.Models;
using Microsoft.AspNetCore.Http;    // zbog IFormFile
using System.IO;                    // potrebno za FileInfo
using Microsoft.AspNetCore.Hosting; //potrebno za IHostingEnvironment
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Prodavnica.Controllers
{
    [Authorize (Roles = "Administrator, ObicanKorisnik")]
    public class AdminController : Controller
    {
        private IProizvodRepozitorijum repozitorijum;
        private IHostingEnvironment hostingEnviroment;
        private UserManager<MojKorisnik> userManager;

        public AdminController(IProizvodRepozitorijum repo, IHostingEnvironment env,
            UserManager<MojKorisnik> usrManager)
        {
            repozitorijum = repo;
            hostingEnviroment = env;
            userManager = usrManager;
        }

        public ViewResult SpisakProizvoda() =>
            View(repozitorijum.Proizvodi);

        [HttpPost]
        public RedirectToActionResult Obrisi(int proizvodID)
        {
            Proizvod proizvod = repozitorijum.BrisiProizvod(proizvodID);

            if (proizvod != null)
            {
                TempData["poruka"] = $"{proizvod.Ime} je obrisan.";
            }

            return RedirectToAction("SpisakProizvoda");
        }

        [HttpGet]
        public ViewResult Izmeni(int proizvodID) =>
            View(repozitorijum.Proizvodi.FirstOrDefault(p => p.ProizvodId == proizvodID));

        [HttpPost]
        public async Task<IActionResult> Izmeni
            ([Bind("ProizvodId, Ime, Opis, Kategorija, Cena")] Proizvod proizvod, 
             IFormFile slika)
        {
            if (ModelState.IsValid)
            {
                await repozitorijum.SacuvajProizvod(proizvod);

                if (slika != null)
                {
                    FileInfo slika_info = new FileInfo(slika.FileName);
                    var newFilename = proizvod.ProizvodId + "_" + 
                        String.Format("{0:d}", (DateTime.Now.Ticks / 10) % 100000000) + 
                        slika_info.Extension;

                    var webPath = hostingEnviroment.WebRootPath;
                    var path = Path.Combine("", webPath + @"\ProizvodiSlike\" + newFilename);
                    var pathToSave = newFilename;
                    
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await slika.CopyToAsync(stream);
                    }
                    proizvod.SlikaPutanja = pathToSave;

                    await repozitorijum.SacuvajProizvod(proizvod);
                }

                TempData["poruka"] = $"{proizvod.Ime} je sačuvan!";
                return RedirectToAction("SpisakProizvoda");
            }
            else
                return View(proizvod);
        }

        public ViewResult Kreiraj() =>
            View("Izmeni", new Proizvod());

        public async Task<ViewResult> Cetovanje()
        {
            /* Podatak o trenutnom korisniku uzimamo iz kontrolera. */
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            MojKorisnik user = await userManager.GetUserAsync(currentUser);

            /* Pogledu zelimo da prosledimo ime trenutnog korisnika.
             * Ukoliko se napise:
             * return View(user.UserName)
             * dolazi do greske!
             * Naime, tip user.UserName je string. View metod kada dobije string
             * ne posmatra ga kao objekat, vec pokusava da u kontroleru nadje metod 
             * cije je naziv  user.UserName i da ga izvrsi.
             * To, naravno, nije ciljno ponasalje. 
             * Postoji nekoliko nacina da se ovaj problem resi.
             * Na primer, ovde je odabrano da se eksplicitno zada ime metoda ("Cetovanje") i potom objekat koji se prosledjuje.
             */
            return View("Cetovanje", user.UserName);
        }
    }
}