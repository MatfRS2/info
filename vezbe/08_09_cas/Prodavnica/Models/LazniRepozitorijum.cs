using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodavnica.Models
{
    public class LazniRepozitorijum /* : IProizvodRepozitorijum */
    {
        public IQueryable<Proizvod> Proizvodi => new List<Proizvod>
        {
            new Proizvod {Ime = "mis", Cena = 20M},
            new Proizvod {Ime = "tastatura", Cena = 50M},
            new Proizvod {Ime = "laptop", Cena = 1500M}
        }.AsQueryable<Proizvod>();
    }
}
