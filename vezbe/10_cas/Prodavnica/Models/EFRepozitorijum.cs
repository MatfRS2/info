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

        public Proizvod BrisiProizvod(int proizvodId)
        {
            Proizvod dbProizvod = repozitorijum.Proizvodi
                .FirstOrDefault(p => p.ProizvodId == proizvodId);

            if (dbProizvod != null)
            {
                repozitorijum.Proizvodi.Remove(dbProizvod);
                repozitorijum.SaveChanges();
            }

            return dbProizvod;
        }

        public async Task SacuvajProizvod(Proizvod proizvod)
        {
            if (proizvod.ProizvodId == 0)
                repozitorijum.Proizvodi.Add(proizvod);
            else
                repozitorijum.Proizvodi.Update(proizvod);

            await repozitorijum.SaveChangesAsync();
        }
    }
}
