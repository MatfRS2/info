

namespace ProdavnicaTehnike.Models{

    public class Ocena{

        public int OcenaID { get; set; }

        public int Vrednost { get; set; }

        public Proizvod Proizvod { get; set; }
    }

}