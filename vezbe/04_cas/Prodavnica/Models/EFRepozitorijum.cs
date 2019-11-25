using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodavnica.Models
{
    public class EFRepozitorijum : IProizvodRepozitorijum
    {
        private ApplicationDbContex repozitorijum;

        public EFRepozitorijum(ApplicationDbContex repo)
        {
            repozitorijum = repo;
        }

        public IQueryable<Proizvod> Proizvodi => repozitorijum.Proizvodi;
    }
}
