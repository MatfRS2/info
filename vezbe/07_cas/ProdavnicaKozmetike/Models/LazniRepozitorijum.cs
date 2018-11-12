using System.Collections.Generic;
using System.Linq;

namespace ProdavnicaKozmetike.Models{

    public class LazniRepozitorijum /*: IProizvodRepozitory */ {
        
        public IQueryable<Proizvod> Proizvodi => new List<Proizvod> {
            new Proizvod { Ime = "Krema za lice", Cena = 650 },
            new Proizvod { Ime = "Maskara", Cena = 1200 },
            new Proizvod { Ime = "Lak za nokte", Cena = 250}
        }.AsQueryable<Proizvod>();

    }

}