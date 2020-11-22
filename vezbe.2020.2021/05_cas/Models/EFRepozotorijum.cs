using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodavnica2.Models
{
    public class EFRepozotorijum : IProizvodRepozitorijum
    {
        private ApplicationDbContext repozitorijum;

        public EFRepozotorijum(ApplicationDbContext repo)
        {
            repozitorijum = repo;
        }

        public IQueryable<Proizvod> Proizvodi => repozitorijum.Proizvodi;
    }
}
