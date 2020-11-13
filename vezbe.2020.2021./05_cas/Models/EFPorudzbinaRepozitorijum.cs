using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodavnica2.Models
{
    public class EFPorudzbinaRepozitorijum : IPorudzbineRepozitorijum
    {
        private ApplicationDbContext repozitorijum;

        public EFPorudzbinaRepozitorijum(ApplicationDbContext repo)
        {
            repozitorijum = repo;
        }

        public IQueryable<Porudzbina> Porudzbine =>
            repozitorijum.Porudzbine.Include(p => p.listaProizvodaUKorpi)
            .ThenInclude(k => k.Proizvod);

        public void SacuvajPorudzbinu(Porudzbina porudzbina)
        {
            repozitorijum.AttachRange(porudzbina.listaProizvodaUKorpi
                .Select(p => p.Proizvod));

            if (porudzbina.PorudzbinaID == 0)
                repozitorijum.Porudzbine.Add(porudzbina);

            repozitorijum.SaveChanges();
        }
    }
}
