using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWeb.Models
{
    public class Skola
    {
        private ProbaContext context;

        public int Id { get; set; }

        public string Naziv { get; set; }

        public string Adresa { get; set; }

    }
}
