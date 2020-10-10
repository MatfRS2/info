using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace Prodavnica.Models.ViewModels
{
    public class KreirajKorisnikaModel
    {
        [Required]
        public string Ime { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Sifra { get; set; }
    }
}
