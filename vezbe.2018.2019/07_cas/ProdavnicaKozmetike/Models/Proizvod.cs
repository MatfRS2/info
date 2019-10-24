using System.Globalization;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace ProdavnicaKozmetike.Models{

    public class Proizvod {
        public int ProizvodID { get; set; }

        [Required (ErrorMessage = "Molimo unesite ime")]
        public string Ime { get; set; }

        [Required (ErrorMessage = "Molimo unesite opis")]
        public string Opis { get; set; }

        [Required]
        [Range (0.01, double.MaxValue, ErrorMessage = "Molimo unesite pozitivnu cenu")]
        public float Cena { get; set; }

        [Required (ErrorMessage = "Molimo unesite kategoriju")]
        public string Kategorija { get; set; }

    }


    public static class Cultures{
    public static readonly CultureInfo Srbija = 
        CultureInfo.GetCultureInfo("sr-Latn-RS");
    }

}