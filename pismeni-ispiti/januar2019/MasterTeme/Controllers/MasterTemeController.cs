using Microsoft.AspNetCore.Mvc;
using MasterTeme.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MasterTeme.Controllers {

	public class MasterTemeController : Controller {
		private ApplicationDbContext repozitorijum;

		public MasterTemeController(ApplicationDbContext repo)
		{
			repozitorijum = repo;
		}
		
		public ViewResult Spisak(string smer="") 
		{
			List<MasterTema> rezultat = repozitorijum.MasterTeme.Include(p => p.Mentor).Include(p => p.Student).
					Include(p => p.Komisija).ThenInclude(p => p.Nastavnik).OrderBy(p => p.DatumNNV).ToList();

			if (smer == null)
			{
				return View(rezultat);
			}
			else
			{
				return View(rezultat.Where(p => p.Student.Smer == smer));
			}
			
		}

		[HttpPost]
        public ViewResult Spisak(string kljucnarec, string opcija)
        {
			if (opcija == null)
				ModelState.AddModelError("", "Opcija nije odabrana");
			if (kljucnarec == null)
				ModelState.AddModelError("", "Nije uneta kljucna rec za pretragu");

			List<MasterTema> rezultat = repozitorijum.MasterTeme.Include(p => p.Mentor).Include(p => p.Student).
					Include(p => p.Komisija).ThenInclude(p => p.Nastavnik).OrderBy(p => p.DatumNNV).ToList();

            if (ModelState.IsValid)
			{
				if (opcija == "mentor")
					return View(rezultat.Where(p => p.Mentor.Ime == kljucnarec || p.Mentor.Prezime == kljucnarec)
					            .OrderBy(p => p.DatumNNV));
				else
					return View(rezultat.Where(p => p.Student.Ime == kljucnarec || p.Student.Prezime == kljucnarec)
					            .OrderBy(p => p.DatumNNV));
			}
			else
				return View(rezultat);
        }
	}
}