using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ProdavnicaKozmetike.Models{

    public static class IdentitySeedData{

        /* Ovako kreiranje admin usera je lose. Za nase potrebe je dovoljno, ali 
         * potrebno je ipak popraviti ponasanje u ozbiljnijim projektima.
         * Pogledati kako se to 
         * ispravno radi na https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-2.2&tabs=linux */
        private const string adminUser = "Daca";
        private const string adminPassword = "Tajna123";
        private const string adminEmail = "admin@mail.com";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            UserManager<AppUser> userManager = app.ApplicationServices.
                GetRequiredService<UserManager<AppUser>>();

            RoleManager<IdentityRole> roleManager = app.ApplicationServices.
                GetRequiredService<RoleManager<IdentityRole>>();

            AppUser user = await userManager.FindByIdAsync(adminUser);
            if (user == null)
            {
                user = new AppUser{
                    UserName = adminUser,
                    Email = adminEmail
                };
                await userManager.CreateAsync(user, adminPassword);
            }

            var roleExist = await roleManager.RoleExistsAsync("Admin");
            if (!roleExist)
            {

                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            roleExist = await roleManager.RoleExistsAsync("Manager");
            if (!roleExist)
            {

                await roleManager.CreateAsync(new IdentityRole("Manager"));
            }

            await userManager.AddToRoleAsync(user, "Admin");
        }

    }

}