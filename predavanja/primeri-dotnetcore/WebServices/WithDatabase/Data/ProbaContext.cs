using HelloWebMvcEF.Models;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;

namespace HelloWebMvcEF.Data
{
  public class ProbaContext : DbContext
  {
    public DbSet<Skola> Skola { get; set; }

    public DbSet<Grad> Grad { get; set; }

    public ProbaContext(DbContextOptions<ProbaContext> options):base(options){
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

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

      //modelBuilder.Entity<Grad>().ToTable("Grad");
      //modelBuilder.Entity<Skola>().ToTable("Skola");
    }
  }
}