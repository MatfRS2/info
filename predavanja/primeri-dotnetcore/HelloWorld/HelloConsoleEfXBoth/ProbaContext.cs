using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;

namespace HelloConsoleEF
{
    public class ProbaContext : DbContext
    {
        public DbSet<Skola> Skola { get; set; }

        public DbSet<Grad> Grad { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseMySQL("server=localhost;port=3307;database=bl_2019_07_skole;user=root;password=");
            optionsBuilder.UseSqlServer("Server=FILIPOVIC07\\TEACHING;Initial Catalog=MatfRs2.Predavanja.Skole01;Integrated Security=false;User id=rs2User;Password=rs2123$;");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Grad>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Naziv).IsRequired();
            });

            modelBuilder.Entity<Skola>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Naziv).IsRequired();
                entity.HasOne(d => d.Grad)
            .WithMany(p => p.Skole);
            });
        }
    }
}