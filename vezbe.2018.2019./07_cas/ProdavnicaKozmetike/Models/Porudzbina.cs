using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ProdavnicaKozmetike.Models{
    public class Porudzbina {

        [BindNever]
        public int PorudzbinaID { get; set; }

        [BindNever]
        public IEnumerable<KorpaElement> listaProizvodaUKorpi { get; set; }

        [BindNever]
        public bool Poslato { get; set; }

        [Required (ErrorMessage = "Molimo Vas unesite ime")]
        public string Ime { get; set; }

        [Required (ErrorMessage = "Molimo Vas unesite barem jednu adresu")]
        public string Adresa1 { get; set; }
        public string Adresa2 { get; set; }

        [Required (ErrorMessage = "Molimo Vas unesite ime grada")]
        public string ImeGrada { get; set; }

        [Required (ErrorMessage = "Molimo Vas unesite državu")]
        public string Drzava { get; set; }

        [Required (ErrorMessage = "Molimo Vas unesite postanski broj")]
        public string PostanskiBroj { get; set; }

        public bool Poklon { get; set; }
    }
}