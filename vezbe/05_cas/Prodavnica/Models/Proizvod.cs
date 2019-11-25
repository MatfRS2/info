using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodavnica.Models
{
    public class Proizvod
    {
        /* Posto zelimo da radimo sa EF moramo imati polje koje je Id 
         * ili TipId -- to ce biti kljuc u bazi. */
        public int ProizvodId { get; set; }
        public string Ime { get; set; }
        public string Opis { get; set; }
        public string Kategorija { get; set; }
        public decimal Cena { get; set; }
        public string SlikaPutanja { get; set; }
    }
}
