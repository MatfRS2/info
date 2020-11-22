using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodavnica2.Models
{
    public class Korpa
    {
        private List<KorpaElement> listaProizvoda = new List<KorpaElement>();

        public IEnumerable<KorpaElement> listaProizvodaUKorpi =>
            listaProizvoda;


        public virtual void ObrisiKorpu() => listaProizvoda.Clear();

        public virtual void DodajProizvod(Proizvod proizvod, int kolicina)
        {
            KorpaElement element = listaProizvoda
                      .Where(p => p.Proizvod.ProizvodID == proizvod.ProizvodID)
                      .FirstOrDefault();

            if (element == null)
            {
                listaProizvoda.Add(new KorpaElement
                {
                    Proizvod = proizvod,
                    Kolicina = kolicina
                });
            }
            else
                element.Kolicina += kolicina;
        }

        public decimal IzracunajVrednost() => listaProizvoda.Sum(p => p.Proizvod.Cena * p.Kolicina);

        public virtual void ObrisiProizvod(Proizvod proizvod) =>
            listaProizvoda.RemoveAll(p => p.Proizvod.ProizvodID == proizvod.ProizvodID);
    }


    public class KorpaElement
    {
        public int KorpaElementID { get; set; }
        public Proizvod Proizvod { get; set; }
        public int Kolicina { get; set; }
    }
}
