using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Prodavnica2.Models.ViewModels
{
    public class SpisakViewModel
    {
        public PageInfo ModelStrane { get; set; }

        public IEnumerable<Proizvod> Proizvodi { get; set; }

        public string TrenutnaKategorija { get; set; }
    }


    public static class Cultures
    {
        public static readonly CultureInfo SrbLtn =
            CultureInfo.GetCultureInfo("sr-Latn-RS");
    }
}
