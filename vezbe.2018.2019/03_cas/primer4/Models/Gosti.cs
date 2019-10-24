using System;
using System.ComponentModel.DataAnnotations; /* Da bih mogla da postavim zeljena ogranicenja za podatke. */

namespace primer4.Models 
{
    public class Gosti
    {
        public int GostID { get; set; }

        [Required(ErrorMessage = "Neophodno uneti ime")]
        public string Ime {get; set;}

        [Required(ErrorMessage = "Potrebno uneti email")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Uneti ispravnu email adresu")]
        public string Email {get; set;}

        [Required(ErrorMessage = "Molimo unesite telefon")]
        [StringLength(15)]
        [RegularExpression("0[0-9]+", ErrorMessage = "Molimo unesite ispravan telefon")]
        public string Telefon {get; set;}

        [Required(ErrorMessage = "Molimo izaberite da li dolazite na zurku ili ne.")]
        public bool? DolaziNaZurku {get; set;}
    }

}