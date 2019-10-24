using primer4.Models;
using System.Collections.Generic;


namespace primer4.Models{

    public interface IRepozitorijum
    {
        IEnumerable<Gosti> svi_gosti { get; }

        void dodajGosta(Gosti gost);
    }

}