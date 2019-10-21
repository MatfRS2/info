using System;
using System.Collections.Generic; /* da bi mogla da koristim listu */

namespace  primer4.Models
{
    public static class Repozitorijum 
    {
        private static List<Gosti> gosti = new List<Gosti>();

        public static IEnumerable<Gosti> svi_gosti
        {
            get {
                return gosti;
            }
        }

        public static void dodajGosta(Gosti gost)
        {
            gosti.Add(gost);
        }
    }
}