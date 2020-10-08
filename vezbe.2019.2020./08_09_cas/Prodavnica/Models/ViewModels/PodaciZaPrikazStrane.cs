using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/* ViewModels pravimo kada zelimo da u View prosledimo nesto sto nije 
 * opisano postojecim modelima (koji opisuju podatke).
 * 
 * Na primer, imamo Proizvod i u View prosledjujemo
 * niz proizvoda i podatak da li su trenutno popusti (znaci neka 2 nepovezana podatka).
 * 
 * Ovo moze da se uradi na vise nacina, ali bi najbolje bilo napraviti klasu
 * koja ima dva polja:
 * -- niz proizvoda
 * -- popust
 * i onda kontroler vraca neku istancu te klase. Ta klasa ne opisuje podatke vec nam
 * je (po logici) vezana za View i zato se naziva ViewModel.
 * 
 * Klasu koju ovde pravimo je potrebna za tag-helper koji pravimo.
 */
namespace Prodavnica.Models.ViewModels
{
    public class PodaciZaPrikazStrane
    {
        public int BrojArtikala { get; set; }
        public int BrojArtikalaPoStrani { get; set; }
        public int TrenutnaStrana { get; set; }

        public int BrojStrana =>
            (int)Math.Ceiling((float)BrojArtikala / BrojArtikalaPoStrani);
    }
}
