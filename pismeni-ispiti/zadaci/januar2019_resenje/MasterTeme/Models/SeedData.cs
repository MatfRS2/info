using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;


namespace MasterTeme.Models {
    public static class SeedData {
        public static void EnsurePopulated(IApplicationBuilder app) 
        {
            ApplicationDbContext context = app.ApplicationServices
                .GetRequiredService<ApplicationDbContext>();

            context.Database.Migrate();

            if (!context.MasterTeme.Any()) 
            {
                var s1 = new Student{Ime = "Danijela", Prezime = "Simic", Indeks = "2004/2009", Smer="I"};
                var s2 = new Student{Ime = "Veljko", Prezime = "Matic", Indeks = "1082/2014", Smer="R"};
                var s3 = new Student{Ime = "Bojan", Prezime = "Markovic", Indeks = "1055/2011", Smer="I"};

                var n1 = new Nastavnik{Ime = "Filip", Prezime = "Maric"};
                var n2 = new Nastavnik{Ime = "Predrag", Prezime = "Janicic"};
                var n3 = new Nastavnik{Ime = "Srdjan", Prezime = "Vukmirovic"};
                var n4 = new Nastavnik{Ime = "Sasa", Prezime = "Malkov"};
                var n5 = new Nastavnik{Ime = "Vladimir", Prezime = "Filipovic"};
                var n6 = new Nastavnik{Ime = "Miodrag", Prezime = "Zivkovic"};
                var n7 = new Nastavnik{Ime = "Aleksandar", Prezime = "Kartelj"};
                var n8 = new Nastavnik{Ime = "Vesna", Prezime = "Marinkovic"};
                var n9 = new Nastavnik{Ime = "Milan", Prezime = "Bankovic"};

                context.MasterTeme.AddRange(
                    new MasterTema {
                        Naziv = "Formalizacija analitičke geometrije",
                        Student = s1,
                        Mentor = n1,
                        Komisija = new List<KomisijaElement> 
                            {new KomisijaElement{Nastavnik = n2}, new KomisijaElement{Nastavnik = n3}},
                        DatumNNV = new DateTime(2009, 10, 5)},
                    new MasterTema {
                        Naziv = "Aplikacija za praćenje autonomnih taksi-vozila",
                        Student = s2,
                        Mentor = n4,
                        Komisija = new List<KomisijaElement> 
                            {new KomisijaElement{Nastavnik = n5}, new KomisijaElement{Nastavnik = n8}},
                        DatumNNV = new DateTime(2018, 9, 14)},
                    new MasterTema {
                        Naziv = "Algoritmi za prepoznavanje gestikulacija rukom",
                        Student = s3,
                        Mentor = n6,
                        Komisija = new List<KomisijaElement> 
                            {new KomisijaElement{Nastavnik = n9}, new KomisijaElement{Nastavnik = n7}},
                        DatumNNV = new DateTime(2016, 9, 9)}  
                );

                context.SaveChanges();
            }
        }
    }
}