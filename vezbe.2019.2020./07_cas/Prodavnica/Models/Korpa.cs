using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodavnica.Models
{
    public class Korpa
    {
        /* Prilikom kreiranja Korpe kreira se prazna korpa,
         * odnosno prazna lista proizvoda.
         */
        private List<KorpaElement> ListaProizvoda = new List<KorpaElement>();

        /* Ovim zelimo da pristupimo trenutnoj listiProizvoda.
         * IEnumerable biramo jer ne mozemo da menjamo listu, tj.
         * ne mozemo nista da dodajemo, samo mozemo da iteriramo kroz nju
         */
        public IEnumerable<KorpaElement> ListaProizvodaUKorpi =>
            ListaProizvoda;

        public void ObrisiKorpu() => ListaProizvoda.Clear();

        public decimal IzracunajVrednost() =>
            ListaProizvoda.Sum(p => p.Proizvod.Cena * p.Kolicina);

        public void DodajProizvod(Proizvod proizvod, int kolicina)
        {
            KorpaElement element = ListaProizvoda
                .Where(p => p.Proizvod.ProizvodId == proizvod.ProizvodId).FirstOrDefault();

            if (element == null)
            {
                ListaProizvoda.Add(
                    new KorpaElement
                    {
                        Proizvod = proizvod,
                        Kolicina = kolicina
                    });
            }
            else
            {
                element.Kolicina += kolicina;
            }
        }

        public void ObrisiProizvod(Proizvod proizvod, int kolicina)
        {
            KorpaElement element = ListaProizvoda
                .Where(p => p.Proizvod.ProizvodId == proizvod.ProizvodId).FirstOrDefault();

            if (element != null && element.Kolicina > kolicina)
                element.Kolicina -= kolicina;
            else
                ListaProizvoda.RemoveAll(p => p.Proizvod.ProizvodId == proizvod.ProizvodId);
        }
    }


    /* Klasa koja opisuje jedan element korpe.
     * Ovo je vazno i sa stanovista baze.
     * Jedna korpa ima vise proizvoda.
     * Ali ako pokusamo da napravimo niz proizvoda u korpi i to mapiramo na bazu
     * pojavice se greska.
     * Problem je sto bi mi pokusali time da imamo samo 2 tabele.
     * Jedna tabela bi bila korpa, a jedna proizvodi.
     * I onda bi jedan red u tabeli korpa trebalo da sadrzi niz
     * kljuceva ka proizvodima, a to u relacionim bazama nije moguce.
     * Zato se prave 3 tabele. Korpa, Proizvodi i VezaIzmedjuKorpeIProizvoda.
     * VezaIzmedjuKorpeIProzivoda ima redove oblika:
     * KorpaId ProizvodId
     * Kod nas VezaIzmedjuKorpeIProzivoda je ova klasa KorpaElement.
     */
    public class KorpaElement
    {
        public int KorpaElementId { get; set; }
        public Proizvod Proizvod { get; set; }
        public int Kolicina { get; set; }
    }
}
