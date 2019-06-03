using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Kontroleri.Models
{
    public class Kontroler
    {
        [BindNever]
        public int KontrolerId { get; set; }

        [Required(ErrorMessage="Molimo unesite ime")]
        public string Ime { get; set; }

        [Required(ErrorMessage="Molimo unesite prezime")]
        public string Prezime { get; set; }

        /* IEnumerable ne dozvoljava promene, a u ovom primeru Raspored treba prosiriti sa novim 
           Vremenima. Ako bi koristili IEnumerable, onda to vreme ne bi uopste bilo upisano. 
           Zato mora lista.
         */
        public List<Vreme> Raspored { get; set; }
    }


    public class Vreme
    {
        public int VremeId { get; set; }

        public DateTime DateTime { get; set; } 

        public RadnaStanica RadnaStanica { get; set; }
    }
}