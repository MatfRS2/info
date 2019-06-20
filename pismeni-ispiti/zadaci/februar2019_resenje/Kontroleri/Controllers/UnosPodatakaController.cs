using Microsoft.AspNetCore.Mvc;
using Kontroleri.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Kontroleri.ViewModels;

namespace Kontroleri.Controllers {

	public class UnosPodatakaController : Controller {
		private ApplicationDbContext repozitorijum;

		public UnosPodatakaController(ApplicationDbContext repo)
		{
			repozitorijum = repo;
		}

        public ViewResult UnosKontrolera()
        {
            return View();
        }

        [HttpPost]
        public ViewResult UnosKontrolera(Kontroler kontroler)
        {
            if (ModelState.IsValid)
            {
                if (repozitorijum.SacuvajKontrolera(kontroler))
                {
                    TempData["message"] = $"{kontroler.Ime} {kontroler.Prezime} je sačuvan!";
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "Kontrolor vec postoji");
                    return View();
                }
            }
            else
                return View();
        }

        public ViewResult DodajRadnoVreme(int kontrolorId)
        {
            return View(
                new KontrolerViewModel{
                    KontorlerId = kontrolorId,
                    RadneStanice = repozitorijum.RadneStanice
                }
            );
        }

        [HttpPost]
        public ViewResult DodajRadnoVreme(KontrolerViewModel kontrolerViewModel)
        {
            if (ModelState.IsValid)
            {
                Vreme nv = new Vreme{
                    RadnaStanica = repozitorijum.RadneStanice
                    .FirstOrDefault(rs => rs.RadnaStanicaId == kontrolerViewModel.RadnaStanicaId),
                    DateTime = kontrolerViewModel.DateTime
                };

                Kontroler dbKontroler = repozitorijum.Kontroleri.Include(p => p.Raspored).ThenInclude(p => p.RadnaStanica)
                    .FirstOrDefault(k => k.KontrolerId == kontrolerViewModel.KontorlerId);
               
                dbKontroler.Raspored.Add(nv);

                //repozitorijum.Add(nv);
                //repozitorijum.Attach(dbKontroler);

                repozitorijum.SaveChanges();

                TempData["message"] = $"Vreme sačuvano!";
            }

            return View(
                new KontrolerViewModel{
                    KontorlerId = kontrolerViewModel.KontorlerId,
                    RadneStanice = repozitorijum.RadneStanice
                }
            );
            
        }
    }
}