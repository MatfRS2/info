using Microsoft.AspNetCore.Mvc;
using ProdavnicaKozmetike.Models;
using ProdavnicaKozmetike.Models.ViewModels;
using System.Linq;

namespace ProdavnicaKozmetike.Controllers {

    public class ProizvodController : Controller {
        private IProizvodRepozitory repozitorijum;

        public int VelicinaStrane = 4;

        public ProizvodController(IProizvodRepozitory repo)
        {
            repozitorijum = repo;
        }

        //1.
        public ViewResult SpisakProizvoda (string kategorija, int brojStraneProizvoda = 1) => View(
            new SpisakProizvodaViewModel{
                Proizvodi = repozitorijum.Proizvodi.
                            Where(p => kategorija == null ? true : p.Kategorija == kategorija).
                            OrderBy(proizvod => proizvod.ProizvodID).
                            Skip((brojStraneProizvoda - 1) * VelicinaStrane).Take(VelicinaStrane),
                ModelStrane = new PageInfo{
                    TrenutnaStrana = brojStraneProizvoda,
                    BrojArtikalaPoStrani = VelicinaStrane,
                    BrojArtikala = kategorija == null ? repozitorijum.Proizvodi.Count() :
                                                        repozitorijum.Proizvodi.Where(p => p.Kategorija == kategorija).Count()
                },
                TrenutnaKategorija = kategorija
            }      
        );
    }

}