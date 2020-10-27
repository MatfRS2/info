using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodavnica2.Models
{
    public class LazniRepozitorijum : IProizvodRepozitorijum
    {
        public IQueryable<Proizvod> Proizvodi => new List<Proizvod> {
            new Proizvod { Ime = "mis", Cena = 200M },
            new Proizvod {Ime = "tastatura", Cena = 200M},
            new Proizvod {Ime = "laptop", Cena = 500M},
            new Proizvod {Ime = "monitor", Cena = 300M}
        }.AsQueryable<Proizvod>();
    }
}
