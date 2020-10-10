using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Prodavnica.Models
{
    public class Proizvod
    {
        /* Posto zelimo da radimo sa EF moramo imati polje koje je Id 
         * ili TipId -- to ce biti kljuc u bazi. */
        public int ProizvodId { get; set; }
        
        [Required (ErrorMessage ="Molimo unesite ime.")]
        public string Ime { get; set; }

        public string Opis { get; set; }

        [Required (ErrorMessage = "Molimo unesite kategoriju.")]
        public string Kategorija { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Molimo unesite pozitivnu cenu.")]
        public decimal Cena { get; set; }

        /* Postavlja se pitanje na koji nacin cuvati slike: u bazi ili ne.
         * Nema jedinstvenog stava, mada bi se reklo da je veca prednost data cuvanju 
         * negde na sistemu, a ne u bazi.
         * Vise o ovoj temi u linku na README.md fajlu uz cas.
         */
        public string SlikaPutanja { get; set; }
    }

    public static class Cultures
    {
        public static readonly CultureInfo Srbija =
            CultureInfo.GetCultureInfo("sr-Latn-RS");
    }
}
