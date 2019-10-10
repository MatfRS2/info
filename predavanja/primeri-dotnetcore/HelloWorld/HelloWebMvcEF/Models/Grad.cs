using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWebMvcEF.Models
{
    public class Grad
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        
        public virtual ICollection<Skola> Skole { get; set; }
    }
}
