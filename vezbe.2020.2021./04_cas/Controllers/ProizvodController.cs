using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prodavnica2.Models;
using Prodavnica2.Models.ViewModels;

namespace Prodavnica2.Controllers
{
    public class ProizvodController : Controller
    {
        private IProizvodRepozitorijum repozitorijum;
        public int VelicinaStrane = 4;

        public ProizvodController(IProizvodRepozitorijum repo)
        {
            repozitorijum = repo;
        }

        public ViewResult Spisak(string kategorija, int Strana = 1) => View(
            new SpisakViewModel
            {
                Proizvodi = repozitorijum.Proizvodi
                        .Where(p => kategorija == null ? true :
                                                         p.Kategorija == kategorija)
                        .OrderBy(proizvod => proizvod.ProizvodID)
                        .Skip((Strana - 1) * VelicinaStrane)
                        .Take(VelicinaStrane),

                ModelStrane = new PageInfo
                {
                    TrenutnaStrana = Strana,
                    BrojArtikalaPoStrani = VelicinaStrane,
                    BrojArtikala = kategorija == null ? 
                                    repozitorijum.Proizvodi.Count() :
                                    repozitorijum.Proizvodi.Where(p => p.Kategorija == kategorija)
                                    .Count()
                                    
                },
                TrenutnaKategorija = kategorija
            }
            );


        //public ViewResult Spisak(int Strana = 1) =>
        //    View(repozitorijum.Proizvodi
        //        .OrderBy(proizvod => proizvod.ProizvodID)
        //        .Skip((Strana - 1) * VelicinaStrane)
        //        .Take(VelicinaStrane));
        

        public ViewResult Spisak2()
        {
            var proizvodi = repozitorijum.Proizvodi.Where(p => p.Cena > 25).ToList<Proizvod>();
            ViewBag.BrojProizvoda = proizvodi.Count();

            return View(proizvodi);
        }
    }
}
