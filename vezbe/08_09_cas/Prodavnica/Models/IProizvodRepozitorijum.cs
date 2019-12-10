using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodavnica.Models
{
    public interface IProizvodRepozitorijum
    {
        IQueryable<Proizvod> Proizvodi { get; }

        Proizvod BrisiProizvod(int proizvodId);
        Task SacuvajProizvod(Proizvod proizvod);
    }
}
