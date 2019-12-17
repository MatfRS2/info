using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Prodavnica.Hubs
{
    public class CetHub : Hub
    {
	/* Ime metoda _PosaljiPoruku_, kao i parametar koji se salje _PrimiPoruku_ se 
         * koristi u okviru javascript koda prilikom poziva metoda.
         */
        public async Task PosaljiPoruku(string korisnik, string poruka)
        {
            await Clients.All.SendAsync("PrimiPoruku", korisnik, poruka);
        }
    }
}
