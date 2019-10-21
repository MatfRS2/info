using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uvod.Models
{
    public class Proizvod
    {
        public int ProizvodID { get; set; }
        public string Ime { get; set; }
        public string Opis { get; set; }
        public string Kategorija { get; set; }
        public decimal Cena { get; set; }
    }
}
