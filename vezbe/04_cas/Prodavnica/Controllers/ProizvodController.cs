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
        public int VelicinaStrane { get; set; } = 4;

        public ProizvodController(IProizvodRepozitorijum repo)
        {
            repozitorijum = repo;
        }

        /* Spisak koji vraca samo VelicinaStrane proizvoda
         * za datu stranu (TekucaStrana). */
        //Resenje 1 
        //Razlicitim stranama pristupamo tako sto za url kucamo
        //https://localhost:______/?tekucaStrana=2
        //ovaj broj na kraju menjamo i pristupamo razlicitim stranama
        /*
        public ViewResult Spisak(int tekucaStrana = 1) =>
            View(repozitorijum.Proizvodi.OrderBy(p => p.ProizvodId).
                 Skip((tekucaStrana-1)*VelicinaStrane).Take(VelicinaStrane));
        */

        //Resenje 2
        //nakon dodavanja tag-hlepera i klasa PodaciZaPrikazStrane i SpisakProizvodaViewModel
        public ViewResult Spisak(int tekucaStrana = 1) =>
            View(new SpisakProizvodaViewModel
            {
                Proizvodi = repozitorijum.Proizvodi.OrderBy(m => m.ProizvodId).
                            Skip((tekucaStrana-1)*VelicinaStrane).Take(VelicinaStrane),
                ModelStrane = new PodaciZaPrikazStrane
                {
                    TrenutnaStrana = tekucaStrana,
                    BrojArtikalaPoStrani = VelicinaStrane,
                    BrojArtikala = repozitorijum.Proizvodi.Count()
                }
            });

    }
}
 