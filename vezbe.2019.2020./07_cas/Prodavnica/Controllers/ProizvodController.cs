using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prodavnica.Models;
using Prodavnica.Models.ViewModels;

namespace Prodavnica.Controllers
{
    public class ProizvodController : Controller
    {
        private IProizvodRepozitorijum repozitorijum;
        public int VelicinaStrane { get; set; } = 2;

        public ProizvodController(IProizvodRepozitorijum repo)
        {
            repozitorijum = repo;
        }

        //Dodajemo kategoriju u Controller
        //Promeniti i BrojArtikala jer sada zavisi od kategorije
        public ViewResult Spisak(string trenutnaKategorija = null, int tekucaStrana = 1) =>
            View(new SpisakProizvodaViewModel
            {
                Proizvodi = repozitorijum.Proizvodi
                            .Where(p => trenutnaKategorija == null
                                   || trenutnaKategorija == ""
                                   || p.Kategorija == trenutnaKategorija)
                            .OrderBy(m => m.ProizvodId)
                            .Skip((tekucaStrana - 1) * VelicinaStrane).Take(VelicinaStrane),
                ModelStrane = new PodaciZaPrikazStrane
                {
                    TrenutnaStrana = tekucaStrana,
                    BrojArtikalaPoStrani = VelicinaStrane,
                    BrojArtikala = repozitorijum.Proizvodi
                                  .Where(p => trenutnaKategorija == null
                                   || trenutnaKategorija == ""
                                   || p.Kategorija == trenutnaKategorija)
                                  .Count()
                },
                TrenutnaKategorija = trenutnaKategorija
            });
    

    }
}
 