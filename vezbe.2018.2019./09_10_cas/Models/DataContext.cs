using Microsoft.EntityFrameworkCore;

namespace ProdavnicaTehnike.Models{

    public class DataContext : DbContext{

        public DataContext(DbContextOptions<DataContext> options)
            :base(options)
        {}

        public DbSet<Proizvod> Proizvodi { get; set; }
        public DbSet<Proizvodjac> Proizvodjaci { get; set; }
        public DbSet<Ocena> Ocene { get; set; }

    }

}