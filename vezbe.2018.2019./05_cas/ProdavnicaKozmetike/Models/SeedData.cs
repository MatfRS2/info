using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;


namespace ProdavnicaKozmetike.Models {
    public static class SeedData {
        public static void EnsurePopulated(IApplicationBuilder app) {
            ApplicationDbContext context = app.ApplicationServices
                .GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            if (!context.Proizvodi.Any()) {
                context.Proizvodi.AddRange(
                    new Proizvod {
                        Ime = "Krema za lice", Opis = "dnevna krema", Kategorija = "nega koze", Cena = 650}                   
                );
                context.SaveChanges();
            }
        }
    }
}