using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ProdavnicaTehnike.Models{

    public class Proizvod{

        [BindNever]
        public int ProizvodID { get; set; }

        [Required]
        public string Ime { get; set; }
        [Required]
        public string Kategorija { get; set; }
        [Required]
        public string Opis { get; set; }
        [Required]
        public decimal Cena { get; set; }

        public Proizvodjac Proizvodjac { get; set; }
        public List<Ocena> Ocene { get; set; }

    }

}