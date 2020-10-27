using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWebMvcEF.Models
{
    public class Skola
    {
         public int Id { get; set; }

        public string Naziv { get; set; }

        public string Adresa { get; set; }

        public int GradId {get; set; }

        public virtual Grad Grad { get; set; }

    }
}
