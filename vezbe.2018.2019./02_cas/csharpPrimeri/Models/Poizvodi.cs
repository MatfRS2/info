namespace csharpPrimeri.Models {
    public class Proizvod {

        //3.1. konstruktor (ako nista nije zadato, podrazumevana vrednost je true)
        public Proizvod (bool naLageru = true)
        {
            NaLageru = naLageru;
        }

        public string Ime {get; set;}

        public decimal? Cena { get; set; }

        //2.1.
        public Proizvod Povezani {get; set;}

        //3.
        public string Kategorija { get; set; } = "Vodeni sportovi";

        //3.1. vrednost koja ne moze da se menja, osim u konstruktoru, sto je zadato gore
        public bool NaLageru { get; }

        public static Proizvod[] nizProizvoda()
        {
            /* M znaci da je broj decimalni, inace ga posmatra kao double */
            Proizvod kajak = new Proizvod { Ime = "kajak", Cena = 276M, Kategorija = "Vodena vozila" };
            Proizvod jakna = new Proizvod (false) { Ime = "jakna", Cena = 48.95M }; /* false se dodaje kad se napravi konstruktor u primeru 3.1. */

            //2.1.
            kajak.Povezani = jakna;

            return new Proizvod[] {kajak, jakna, null};
        }

    }

}