using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Prodavnica.Models
{
    public class MojKorisnik : IdentityUser
    {
        /* ASP.NET Core nudi mehanizme za rad sa korisnicima. 
         * Implementirane su najcesce koriscene operacije sa korisnicima.
         * Ipak, u novoj klasi (ovde je to MojKorisnik) mogu se dodati nove
         * mogucnosti.
         * Vise o klasi i njenim poljima: https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.entityframeworkcore.identityuser?view=aspnetcore-1.1
         */
         /* Za sada ne dodajemo nista novo u klasu MojKorisnik.
          * Mogli smo i da je ne pravimo.
          */
    }
}
