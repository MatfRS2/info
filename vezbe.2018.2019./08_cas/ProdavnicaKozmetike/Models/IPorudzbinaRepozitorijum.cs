using System.Collections.Generic;
using System.Linq;

namespace ProdavnicaKozmetike.Models
{
    public interface IPorudzbineRepozitorijum
    {
        IQueryable<Porudzbina> Porudzbine { get; }
        void SacuvajPorudzbinu (Porudzbina porudzbina);
    } 
}