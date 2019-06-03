using Microsoft.AspNetCore.Mvc;
using MasterTeme.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using MasterTeme.ViewModels;

namespace MasterTeme.Controllers {

	public class UnosPodatakaController : Controller {
		private ApplicationDbContext repozitorijum;

		public UnosPodatakaController(ApplicationDbContext repo)
		{
			repozitorijum = repo;
		}

        public ViewResult UnosNastavnika()
        {
            return View();
        }

        [HttpPost]
        public ViewResult UnosNastavnika(Nastavnik nastavnik)
        {
            if (ModelState.IsValid)
            {
                repozitorijum.SacuvajNastavnika(nastavnik);
                TempData["message"] = $"{nastavnik.Ime} {nastavnik.Prezime} je sačuvan!";
                return View();
            }
            else
                return View();
        }

        public ViewResult UnosStudenta()
        {
            return View();
        }

        [HttpPost]
        public ViewResult UnosStudenta(Student student)
        {
            if (ModelState.IsValid)
            {
                if (repozitorijum.SacuvajStudenta(student))
                    TempData["message"] = $"{student.Ime} {student.Prezime} je sačuvan!";
                else
                    ModelState.AddModelError("", "Student sa datim indeksom vec postoji");
                return View();
            }
            else
                return View();
        }

        public ViewResult UnosMasterTeme()
        {
            return View(new MasterTemaViewModel{Nastavnici = repozitorijum.Nastavnici});
        }

        [HttpPost]
        public ViewResult UnosMasterTeme(MasterTemaViewModel masterViewTema)
        {
            if (ModelState.IsValid)
            {
                Student s1 = repozitorijum.Studenti.FirstOrDefault(p => p.Indeks == masterViewTema.Indeks);

                if (s1 == null)
                    ModelState.AddModelError("", "Master tema nije uspesno sacuvana, student ne postoji");
                else
                {

                    MasterTema masterTema = new MasterTema{
                        Naziv = masterViewTema.Naziv,
                        Student = s1,
                        Mentor =  repozitorijum.Nastavnici.FirstOrDefault(p => p.NastavnikId == masterViewTema.NastavnikId),
                        DatumNNV = masterViewTema.DatumNNV
                    };

                    List<KomisijaElement> komisija = new List<KomisijaElement>();
                    foreach(var nastavnikId in masterViewTema.KomisijaId)
                    {
                        komisija.Add(new KomisijaElement{
                            Nastavnik = repozitorijum.Nastavnici.FirstOrDefault(p => p.NastavnikId == nastavnikId)});
                    }
                    masterTema.Komisija = komisija.AsEnumerable();

                    if (repozitorijum.SacuvajMasterTemu(masterTema))
                        TempData["message"] = $"Tema {masterTema.Naziv} je sačuvana!";
                    else
                        ModelState.AddModelError("", "Master tema nije uspesno sacuvana");
                }
                return View(new MasterTemaViewModel{Nastavnici = repozitorijum.Nastavnici});
            }
            else
                return View(new MasterTemaViewModel{Nastavnici = repozitorijum.Nastavnici});
        }
    }
}