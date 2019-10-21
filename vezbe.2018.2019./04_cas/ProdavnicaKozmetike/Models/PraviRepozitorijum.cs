using System.Collections.Generic;
using System.Linq;

namespace ProdavnicaKozmetike.Models {

    public class PraviRepozitorijum : IProizvodRepozitory{
        private ApplicationDbContext repozitorijum;

        public PraviRepozitorijum(ApplicationDbContext repo)
        {
            repozitorijum = repo;
        }

        public IQueryable<Proizvod> Proizvodi => repozitorijum.Proizvodi;

    }

}