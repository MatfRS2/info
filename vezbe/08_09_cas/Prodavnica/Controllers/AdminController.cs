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

namespace Prodavnica.Controllers
{
    [Authorize (Roles = "Administrator, ObicanKorisnik")]
    public class AdminController : Controller
    {
        private IProizvodRepozitorijum repozitorijum;
        /* Slike su smestene u wwwroot/ folderu i mozemo ih odatle citati/cuvati.
         * Medjutim, nije dobro da se kroz aplikaciju svuda vuce ta putanja.
         * Koristimo IHostingEnvironment koja sadrzi neophodne putanje
         * vec podesene.
         * A u kontrolerima joj imamo pristup preko IHostingEnviroment (pomocu model bindinga) 
         * 
         * U .Core 3.0 nije IHostingEnviroment vec IWebHostEnvironment
         * Upotreba je ista.
         */
        private IHostingEnvironment hostingEnviroment;

        public AdminController(IProizvodRepozitorijum repo, IHostingEnvironment env)
        {
            repozitorijum = repo;
            hostingEnviroment = env;
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

        /* Da bi se sprecile zloupotrebe i napadi dobro je dodati
         * atribut Bind.
         * 
         * Ucitavanje dokumenta je veoma osteljivo gledajuci iz ugla bezbednosti.
         * Moguce je ucitati dokumenta koja nisu slike i koja mogu da 
         * pokupe razne podatke sa servera i prekinu njegov rad.
         * 
         * Zato se posebna paznja poklanja ispitivanju da li je ucitani fajl
         * korektan. Prvo se proverava ekstenzija, velicina, a zatim i drugi detalji.
         * Vise o svemu ovome se moze naci na interentu.
         * 
         * U ovom primeru se nije obracala paznja na ispitivanje bezbednosti,
         * ali u svakom ozbiljnijoj aplikaciji to je neophodno!
         */
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
    }
}