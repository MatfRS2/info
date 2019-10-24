using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProdavnicaTehnike.Models;

namespace ProdavnicaTehnike{

    public class SeedData{

        public static void SeedDatabase(DataContext context)
        {
            context.Database.Migrate();
            if (context.Proizvodi.Count() == 0)
            {
                var s1 = new Proizvodjac { Ime = "HP",
                    Grad = "San Jose", Drzava = "US"};
                var s2 = new Proizvodjac { Ime = "Lenovo",
                    Grad = "Chicago", Drzava = "US"};
                var s3 = new Proizvodjac { Ime = "Samsung",
                    Grad = "Seul", Drzava = "Juzna Koreja"};      

                context.Proizvodi.AddRange(
                    new Proizvod { Ime = "HP Probook",
                         Opis = "13 inca",
                         Kategorija = "laptop", Cena = 1275, Proizvodjac = s1,
                         Ocene = new List<Ocena> {
                             new Ocena { Vrednost = 4 }, new Ocena { Vrednost = 3 }}},
                    new Proizvod { Ime = "HP EliteOne",
                         Opis = "21 inca",
                         Kategorija = "stoni racunar", Cena = 1500, Proizvodjac = s1,
                         Ocene = new List<Ocena> {
                             new Ocena { Vrednost = 2 }, new Ocena { Vrednost = 5 }}},                     
                    new Proizvod { Ime = "Lenovo ThinkPad",
                         Opis = "13 inca",
                         Kategorija = "laptop", Cena = 1400, Proizvodjac = s2,
                         Ocene = new List<Ocena> {
                             new Ocena { Vrednost = 1 }, new Ocena { Vrednost = 3 }}},
                    new Proizvod { Ime = "M Series",
                         Opis = "kuciste",
                         Kategorija = "stoni racunar", Cena = 319, Proizvodjac = s2,
                         Ocene = new List<Ocena> { new Ocena { Vrednost = 3 }}},
                    new Proizvod { Ime = "Lenovo Yoga",
                         Opis = "14 inca",
                         Kategorija = "laptop", Cena = 715, Proizvodjac = s3,
                         Ocene = new List<Ocena> {
                             new Ocena { Vrednost = 1 }, new Ocena { Vrednost = 4 }, new Ocena { Vrednost = 4 }}},
                    new Proizvod { Ime = "Samsung s9",
                         Opis = "6.5 inca",
                         Kategorija = "telefon", Cena = 1100, Proizvodjac = s3,
                         Ocene = new List<Ocena> {
                             new Ocena { Vrednost = 4 }, new Ocena { Vrednost = 5 }}},
                    new Proizvod { Ime = "Samsung QLED",
                         Opis = "8KT",
                         Kategorija = "TV", Cena = 800, Proizvodjac = s3,
                         Ocene = new List<Ocena> { new Ocena { Vrednost = 3 }}},
                    new Proizvod { Ime = "Notebook 9",
                         Opis = "13 inca",
                         Kategorija = "laptop", Cena = 1199, Proizvodjac = s3},
                    new Proizvod { Ime = "Chef Collection",
                         Opis = "Ugradna rerna",
                         Kategorija = "bela tehnika", Cena = 3199, Proizvodjac = s3});

                    context.SaveChanges();                
            }
        }
    }

}