using System;

namespace ProdavnicaKozmetike.Models.ViewModels{

    public class PageInfo{
        public int BrojArtikala { get; set; }
        public int BrojArtikalaPoStrani { get; set; }
        public int TrenutnaStrana { get; set; }

        public int BrojStrana =>
            (int)Math.Ceiling((float) BrojArtikala/BrojArtikalaPoStrani);
    }

}