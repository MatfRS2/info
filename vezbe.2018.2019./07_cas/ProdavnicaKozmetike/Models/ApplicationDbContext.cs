using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace ProdavnicaKozmetike.Models{

    public class ApplicationDbContext : DbContext {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opcije)
            : base (opcije) {}

        public DbSet<Proizvod> Proizvodi { get; set; }
        public DbSet<Porudzbina> Porudzbine { get; set; }

    }

}