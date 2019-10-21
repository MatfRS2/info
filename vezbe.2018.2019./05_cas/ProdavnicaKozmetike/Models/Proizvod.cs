using System.Globalization;


namespace ProdavnicaKozmetike.Models{

    public class Proizvod {
        public int ProizvodID { get; set; }

        public string Ime { get; set; }

        public string Opis { get; set; }

        public float Cena { get; set; }

        public string Kategorija { get; set; }

    }


    public static class Cultures{
    public static readonly CultureInfo Srbija = 
        CultureInfo.GetCultureInfo("sr-Latn-RS");
    }

}