using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace MasterTeme.Models{

    public class ApplicationDbContext : DbContext {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opcije)
            : base (opcije) {}

        public DbSet<MasterTema> MasterTeme { get; set; }
        public DbSet<Nastavnik> Nastavnici { get; set; }
        public DbSet<Student> Studenti { get; set; }

        public void SacuvajNastavnika(Nastavnik nastavnik){

            Nastavnik dbNastavnik = Nastavnici.FirstOrDefault(p => p.Ime == nastavnik.Ime && p.Prezime == nastavnik.Prezime);

            if (nastavnik.NastavnikId == 0)
                Nastavnici.Add (nastavnik);

            SaveChanges();  
        }

        public bool SacuvajStudenta(Student student){

            Student dbStudent = Studenti.FirstOrDefault(p => p.Indeks == student.Indeks);

            if (dbStudent == null)
            {
                Studenti.Add (student);
                SaveChanges(); 
                return true;
            }
            else
                return false;
 
        }

        public bool SacuvajMasterTemu(MasterTema masterTema){

            MasterTema dbMaster = MasterTeme.FirstOrDefault(p => p.Student == masterTema.Student);

            AttachRange(masterTema.Komisija.Select(p => p.Nastavnik));

            if (dbMaster == null)
            {
                MasterTeme.Add(masterTema);
                SaveChanges(); 
                return true;
            }
            else
                return false;
 
        }
    }

}