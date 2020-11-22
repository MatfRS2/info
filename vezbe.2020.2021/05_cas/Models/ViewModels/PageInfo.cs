using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodavnica2.Models.ViewModels
{
    public class PageInfo
    {
        public int BrojArtikala { get; set; }
        public int BrojArtikalaPoStrani { get; set; }
        public int TrenutnaStrana { get; set; }

        public int BrojStrana =>
            (int)Math.Ceiling((float)BrojArtikala / BrojArtikalaPoStrani);
    }
}
