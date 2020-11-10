using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Prodavnica2.Models
{
    public class Porudzbina
    {
        [BindNever]
        public int PorudzbinaID { get; set; }
        [BindNever]
        public IEnumerable<KorpaElement> listaProizvodaUKorpi { get; set; }

        [Required (ErrorMessage = "Molimo unesite Vase ime")]
        public string Ime { get; set; }

        [Required (ErrorMessage = "Molimo unesite barem jednu adresu")]
        public string Adresa1 { get; set; }
        public string Adresa2 { get; set; }

        [Required (ErrorMessage = "Molimo unesite ime grada")]
        public string ImeGrada { get; set; }

        [Required(ErrorMessage = "Molimo unesite ime drzave")]
        public string Drzava { get; set; }

        [Required(ErrorMessage = "Molimo unesite postanski broj")]
        public string PostanskiBroj { get; set; }
        public bool Poklon { get; set; }
    }
}
