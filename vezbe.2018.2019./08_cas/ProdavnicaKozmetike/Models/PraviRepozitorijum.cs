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


        public void SacuvajProizvod(Proizvod proizvod){

            Proizvod dbProizvod = repozitorijum.Proizvodi.FirstOrDefault(p => p.ProizvodID == proizvod.ProizvodID);

            if (proizvod.ProizvodID == 0)
                repozitorijum.Proizvodi.Add (proizvod);
            else
            {
                dbProizvod.Ime = proizvod.Ime;
                dbProizvod.Opis = proizvod.Opis;
                dbProizvod.Cena = proizvod.Cena;
                dbProizvod.Kategorija = proizvod.Kategorija;
            }

            repozitorijum.SaveChanges();  
        }

        public Proizvod BrisiProizvod(int proizvodID)
        {
            Proizvod dbProizvod = repozitorijum.Proizvodi.FirstOrDefault(p => p.ProizvodID == proizvodID);

            if (dbProizvod != null)
            {
                repozitorijum.Proizvodi.Remove(dbProizvod);
                repozitorijum.SaveChanges();
            }

            return dbProizvod;
        }

    }

}