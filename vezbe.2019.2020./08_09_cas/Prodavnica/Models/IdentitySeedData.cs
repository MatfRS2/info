using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Prodavnica.Models
{
    public static class IdentitySeedData
    {
        public static async void DodajDefaultRole(IApplicationBuilder app)
        {
            UserManager<MojKorisnik> userManager =
               app.ApplicationServices.GetRequiredService<UserManager<MojKorisnik>>();

            RoleManager<IdentityRole> roleManager =
                app.ApplicationServices.GetRequiredService<RoleManager<IdentityRole>>();

            /* Pretpostavljamo da je u bazu vec dodat korisnik
             * ciji email je danijela.p9@gmail.com.
             * Ovo je dodato prilikom testiranja metoda KreirajKorisnika
             * u Account kontroleru.
             */
            MojKorisnik korisnik = await userManager.FindByEmailAsync("danijela.p9@gmail.com");

            if (korisnik == null)
            {
                //ako je postoji, trebalo bi ga napraviti
                //ali u ovom slucaju pretpostavljamo "da je ulaz ispravan"
                //odnosno korisnik vec postoji
            }

            /* Ako u bazi ne postoje kreiramo dve role.
             */
            var roleExist = await roleManager.RoleExistsAsync("Administrator");
            if (!roleExist)
                await roleManager.CreateAsync(new IdentityRole("Administrator"));

            roleExist = await roleManager.RoleExistsAsync("ObicanKorisnik");
            if (!roleExist)
                await roleManager.CreateAsync(new IdentityRole("ObicanKorisnik"));

            await userManager.AddToRoleAsync(korisnik, "Administrator");
        }
    }
}
