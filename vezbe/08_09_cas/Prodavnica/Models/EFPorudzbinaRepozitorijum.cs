using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; //Zbog Include i ThenInclude itd...

namespace Prodavnica.Models
{
    public class EFPorudzbinaRepozitorijum : IPorudzbineRepozitorijum
    {
        private ApplicationDbContex repozitorijum;

        public EFPorudzbinaRepozitorijum(ApplicationDbContex repo)
        {
            repozitorijum = repo;
        }

        public IQueryable<Porudzbina> Porudzbine =>
            repozitorijum.Porudzbine.Include(p => p.ListaProizvodaUKorpi)
            .ThenInclude(k => k.Proizvod);

        public void SacuvajPorudzbinu (Porudzbina porudzbina)
        {
            repozitorijum.AttachRange
                (porudzbina.ListaProizvodaUKorpi.Select(p => p.Proizvod));

            if (porudzbina.PorudzbinaId == 0)
                repozitorijum.Porudzbine.Add(porudzbina);

            repozitorijum.SaveChanges();
        }
    }
}
