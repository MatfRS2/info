using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Prodavnica.Models.ViewModels
{
    public class PrijavljivanjeModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [UIHint("sifra")]
        public string Sifra { get; set; }
    }
}
