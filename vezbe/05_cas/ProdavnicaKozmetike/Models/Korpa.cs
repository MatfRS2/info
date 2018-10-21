using System.Collections.Generic;
using System.Linq;

namespace ProdavnicaKozmetike.Models{

    public class Korpa{

        private List<KorpaElement> listaProizvoda = new List<KorpaElement>();

        public IEnumerable<KorpaElement> listaProizvodaUKorpi => listaProizvoda;

        public void ObrisiKorpu() => listaProizvoda.Clear();

        public double IzracunajVrednost() => listaProizvoda.Sum(p => p.Proizvod.Cena * p.Kolicina);

        public void ObrisiProizvod(Proizvod proiz) => 
           listaProizvoda.RemoveAll(p => p.Proizvod.ProizvodID == proiz.ProizvodID);

        public void DodajProizvod(Proizvod proizvod, int kolicina)
        {
            KorpaElement element = listaProizvoda.Where(p => p.Proizvod.ProizvodID == proizvod.ProizvodID).FirstOrDefault();

            if (element == null)
            {
                listaProizvoda.Add(new KorpaElement {
                    Proizvod = proizvod,
                    Kolicina = kolicina
                });
            }
            else
                element.Kolicina += kolicina;
        }

    } 
    
    public class KorpaElement{
        public int KorpaElementID { get; set; }
        public Proizvod Proizvod { get; set; }
        public int Kolicina { get; set; }
    }
}