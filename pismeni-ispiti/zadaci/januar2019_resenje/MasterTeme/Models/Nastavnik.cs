using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MasterTeme.Models{

    public class Nastavnik
    {
        public int NastavnikId { get; set; }

        [Required(ErrorMessage="Molimo Vas unesite ime")]
        public string Ime { get; set; }

        [Required(ErrorMessage="Molimo Vas unesite prezime")]
        public string Prezime { get; set; }
        
    }
}