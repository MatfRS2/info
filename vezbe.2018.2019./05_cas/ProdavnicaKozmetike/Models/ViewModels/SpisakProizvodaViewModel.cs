using System.Collections.Generic;

namespace ProdavnicaKozmetike.Models.ViewModels{

    public class SpisakProizvodaViewModel{

        public PageInfo ModelStrane { get; set; }

        public IEnumerable<Proizvod> Proizvodi { get; set; }

        //1.
        public string TrenutnaKategorija { get; set; }

    }
}