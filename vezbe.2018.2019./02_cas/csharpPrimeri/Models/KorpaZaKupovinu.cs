using System.Collections.Generic; /* Da bi mogla da koristim IEnumerable */
using System; /* Da bi mogli da koristim Lambda izraze */

namespace csharpPrimeri.Models {

    public class KorpaZaKupovinu {
        public IEnumerable<Proizvod> Proizvodi { get; set; }

        public decimal ukupnaCena ()
        {
            decimal zbir = 0;

            foreach (Proizvod p in Proizvodi)
            {
                zbir += p?.Cena ?? 0;
            }

            return zbir;
        }


        /* I nacin
        public IEnumerable<Proizvod> filtrirajPoCeni (decimal minimalnaCena)
        {
            List<Proizvod> rezultat = new List<Proizvod>();

            foreach (Proizvod p in Proizvodi)
            {
                if ((p?.Cena ?? 0) >= minimalnaCena)
                    rezultat.Add(p);
            }

            return rezultat;
        }
        */

        /* II nacin, koriscenjem yield */
        public IEnumerable<Proizvod> filtrirajPoCeni (decimal minimalnaCena)
        {
            foreach (Proizvod p in Proizvodi)
            {
                if ((p?.Cena ?? 0) >= minimalnaCena)
                    yield return p;
            }
        }

        public IEnumerable<Proizvod> filtrirajPoImenu (char c)
        {
            foreach (Proizvod p in Proizvodi)
            {
                if (p?.Ime?[0] == c)
                    yield return p;
            }
        }


        /* III nacin -- koriscenjem lambda izraza (prosledjujemo funkciju kao parametar) */
        public IEnumerable<Proizvod> filtriraj (Func<Proizvod, bool> selektor)
        {
            foreach (Proizvod p in Proizvodi)
            {
                if (selektor(p))
                    yield return p;
            }
        }
        

    }

}