using System;
using System.Collections.Generic; /* da bi mogla da koristim listu */

namespace  primer4.Models
{
    public class Repozitorijum : IRepozitorijum
    {
        private List<Gosti> gosti = new List<Gosti>();

        public IEnumerable<Gosti> svi_gosti
        {
            get {
                return gosti;
            }
        }

        public void dodajGosta(Gosti gost)
        {
            gosti.Add(gost);
        }
    }
}