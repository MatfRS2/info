using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; //zbog [Required ...
using Microsoft.AspNetCore.Mvc.ModelBinding; //potrebno da bi lako povezali Model i Razor komponente


namespace Prodavnica.Models
{
    public class Porudzbina
    {
        [BindNever]
        public int PorudzbinaId { get; set; }

        [BindNever]
        public IEnumerable<KorpaElement> ListaProizvodaUKorpi { get; set; }

        [BindNever]
        public bool Poslato { get; set; }

        [Required (ErrorMessage = "Molimo Vas unesite ime")]
        public string Ime { get; set; }

        [Required (ErrorMessage = "Molimo Vas unesite barem jednu adresu.")]
        public string Adresa1 { get; set; }
        public string Adresa2 { get; set; }

        [Required (ErrorMessage = "Molimo Vas unesite ime grada")]
        public string ImeGrada { get; set; }

        [Required (ErrorMessage = "Molimo Vas unesite drzavu")]
        public string  Drzava { get; set; }

        [Required (ErrorMessage = "Molimo Vas unesite postanski broj")]
        public string PostanskiBroj { get; set; }

        public bool Poklon { get; set; }
    }
}
