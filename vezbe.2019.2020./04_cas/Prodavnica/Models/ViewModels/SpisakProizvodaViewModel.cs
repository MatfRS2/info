using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
 * Koristi se da bi se potrebni podaci preneli u Razor stranicu.
*/
namespace Prodavnica.Models.ViewModels
{
    public class SpisakProizvodaViewModel
    {
        public IEnumerable<Proizvod> Proizvodi { get; set; }
        public PodaciZaPrikazStrane ModelStrane { get; set; }
    }
}
