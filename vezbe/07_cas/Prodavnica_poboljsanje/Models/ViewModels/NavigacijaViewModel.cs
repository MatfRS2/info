using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodavnica.Models.ViewModels
{
    public class NavigacijaViewModel
    {
        public IEnumerable<string> Kategorije { get; set; }
        public string TrenutnaKategorija { get; set; }
    }
}
