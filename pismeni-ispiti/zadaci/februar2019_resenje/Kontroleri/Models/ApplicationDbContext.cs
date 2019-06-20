using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Kontroleri.Models{

    public class ApplicationDbContext : DbContext {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opcije)
            : base (opcije) {}

        public DbSet<Kontroler> Kontroleri { get; set; }

        public DbSet<RadnaStanica> RadneStanice { get; set; }

        public bool SacuvajKontrolera(Kontroler kontroler){

            Kontroler dbKontroler = Kontroleri.FirstOrDefault(p => p.Ime == kontroler.Ime && p.Prezime == kontroler.Prezime);

            if (dbKontroler == null)
            {
                Kontroleri.Add (kontroler);
                SaveChanges();  
                return true;
            }
            else
                return false;
        }
    }
}