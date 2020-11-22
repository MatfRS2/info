using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodavnica2.Models
{
    public interface IPorudzbineRepozitorijum
    {
        IQueryable<Porudzbina> Porudzbine { get; }
        void SacuvajPorudzbinu(Porudzbina porudzbina);
    }
}
