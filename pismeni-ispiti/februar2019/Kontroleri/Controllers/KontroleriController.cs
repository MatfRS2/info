using Microsoft.AspNetCore.Mvc;
using Kontroleri.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace Kontroleri.Controllers {

	public class KontroleriController : Controller {
        private ApplicationDbContext repozitorijum;
		public KontroleriController(ApplicationDbContext repo)
		{
			repozitorijum = repo;
		}


		public ViewResult Spisak() 
		{
			return View(repozitorijum.Kontroleri.OrderBy(k => k.Prezime).ThenBy(k => k.Ime));				
		}

		[HttpPost]
        public ViewResult Spisak(DateTime datum)
        {
			return View(repozitorijum.Kontroleri.Include(p => p.Raspored)
				.Where(k => k.Raspored.Where(r => r.DateTime.Date == datum.Date).Count() > 0)
				.OrderBy(k => k.Prezime).ThenBy(k => k.Ime));
		}


		public ViewResult Informacije(int kontrolorId)
		{
			return View(repozitorijum.Kontroleri.Include(p => p.Raspored).ThenInclude(p => p.RadnaStanica)
			   .FirstOrDefault(k => k.KontrolerId == kontrolorId));
		}
    }
}