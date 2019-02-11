using System.Collections.Generic;
using Kontroleri.Models;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Kontroleri.ViewModels{

    public class KontrolerViewModel
    {
        
        public int KontorlerId { get; set; }

        [Required(ErrorMessage = "Molimo izaberite radnu stanicu")]
        public int RadnaStanicaId { get; set; }

        [Required(ErrorMessage = "Molimo izaberite datum i vreme")]
        public DateTime DateTime { get; set; }

        public IEnumerable<RadnaStanica> RadneStanice { get; set; }
    }

}