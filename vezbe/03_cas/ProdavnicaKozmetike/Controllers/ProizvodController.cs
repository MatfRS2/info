using Microsoft.AspNetCore.Mvc;
using ProdavnicaKozmetike.Models;
using ProdavnicaKozmetike.Models.ViewModels;

//23.
using System.Linq;

namespace ProdavnicaKozmetike.Controllers {

    public class ProizvodController : Controller {
        private IProizvodRepozitory repozitorijum;

        //23.
        public int VelicinaStrane = 4;

        public ProizvodController(IProizvodRepozitory repo)
        {
            repozitorijum = repo;
        }
        
        //public ViewResult SpisakProizvoda() => View(repozitorijum.Proizvodi);

        /*23.
        public ViewResult SpisakProizvoda(int brojStraneProizvoda = 1) => View(
            repozitorijum.Proizvodi.OrderBy(proizvod => proizvod.ProizvodID).
            Skip((brojStraneProizvoda - 1) * VelicinaStrane).Take(VelicinaStrane));
        */

        //28.
        public ViewResult SpisakProizvoda (int brojStraneProizvoda = 1) => View(
            new SpisakProizvodaViewModel{
                Proizvodi = repozitorijum.Proizvodi.OrderBy(proizvod => proizvod.ProizvodID).
                            Skip((brojStraneProizvoda - 1) * VelicinaStrane).Take(VelicinaStrane),
                ModelStrane = new PageInfo{
                    TrenutnaStrana = brojStraneProizvoda,
                    BrojArtikalaPoStrani = VelicinaStrane,
                    BrojArtikala = repozitorijum.Proizvodi.Count()
                }
            }      
        );
    }

}