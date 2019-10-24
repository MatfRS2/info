using System.Collections.Generic;

namespace ProdavnicaTehnike.Models{

    public class Proizvodjac{

        public int ProizvodjacID { get; set; }

        public string Ime { get; set; }
        public string Grad { get; set; }
        public string Drzava { get; set; }

        public IEnumerable<Proizvod> Proizvodi { get; set; }
    }

}