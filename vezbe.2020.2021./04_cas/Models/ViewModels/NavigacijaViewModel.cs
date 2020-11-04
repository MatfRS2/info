using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodavnica2.Models.ViewModels
{
    public class NavigacijaViewModel
    {
        public  IEnumerable<string> Kategorije { get; set; }

        public string TrenutnaKategorija { get; set; }
    }
}
