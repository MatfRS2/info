
namespace linqIzrazi{
    public class Osoba {
        public string Ime { get; set; }
        public string Pol { get; set; }
        public int GodinaRodjenja { get; set; }

        public override string ToString()
        {
            return Ime + ", " + Pol + ", " + GodinaRodjenja;
        }
    }

    
}