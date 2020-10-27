using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;

namespace HelloConsoleEF
{
    class Program
    {

        static void PrikaziMeni()
        {
            Console.WriteLine("Izaberi opciju:");
            Console.WriteLine("1. Prikaz podataka o svim gradovima");
            Console.WriteLine("2. Prikaz podataka o svim skolama");
            Console.WriteLine("3. Prikaz podataka o skolama iz datog grada");
            Console.WriteLine("4. Dodavanje grada");
            Console.WriteLine("5. Dodavanje skole");
            Console.WriteLine("x. Izlaz");
        }

        static void Main(string[] args)
        {
            while (true)
            {
                PrikaziMeni();
                char izbor = Console.ReadLine().Trim().ToUpper()[0];
                if (izbor == 'X')
                    return;
                switch (izbor)
                {
                    case '1':
                        Console.WriteLine("Prikaz podataka iz baze:");
                        GradoviNaKonzolu();
                        break;
                    case '2':
                        Console.WriteLine("Prikaz podataka iz baze:");
                        SkoleNaKonzolu();
                        break;
                    case '3':
                        Console.WriteLine("Unesite ime grada.");
                        string imeGrada = Console.ReadLine().Trim();
                        Console.WriteLine($"\nSkole iz grada {imeGrada} su: \n");
                        SkoleNaKonzolu(imeGrada);
                        break;
                    case '4':
                        Console.WriteLine("Unesite ime grada.");
                        string imeGrada1 = Console.ReadLine();
                        DodajGrad(imeGrada1);
                        break;
                    case '5':
                        Console.WriteLine("Unesite ime skole.");
                        string imeSkole = Console.ReadLine();
                        Console.WriteLine("Unesite adresu skole.");
                        string adresaSkole = Console.ReadLine();
                        GradoviNaKonzolu();
                        Console.WriteLine("Unesite id grada koji je ponudjen.");
                        int idGrada = Convert.ToInt32(Console.ReadLine());
                        DodajSkolu(imeSkole, adresaSkole, idGrada);
                        break;
                    default:
                        Console.WriteLine("Opcija ne postoji.");
                        break;
                }
            }
        }


        private static void GradoviNaKonzolu()
        {
            using (var context = new ProbaContext())
            {
                var gradovi = context.Grad;
                var data = new StringBuilder();
                foreach (Grad g in gradovi)
                {
                    data.AppendLine($"Naziv grada: {g.Naziv}");
                    data.AppendLine($"Id grada: {g.Id}");
                }
                Console.WriteLine(data.ToString());
            }
        }
        private static void SkoleNaKonzolu()
        {
            using (var context = new ProbaContext())
            {
                var skole = context.Skola.Include(p => p.Grad);
                var data = new StringBuilder();
                foreach (Skola s in skole)
                {
                    data.AppendLine($"Naziv: {s.Naziv}");
                    data.AppendLine($"Adresa: {s.Adresa}");
                    data.AppendLine($"Grad: {s.Grad.Naziv}");
                }
                Console.WriteLine(data.ToString());
            }

        }

        private static void SkoleNaKonzolu(string imeGrada)
        {
            using (var context = new ProbaContext())
            {
                var listaGradId = context.Grad.Where(x => x.Naziv.Equals(imeGrada))
                .Select(x => x.Id).ToList();
                if (listaGradId.Count <= 0)
                {
                    Console.WriteLine("Ovog grada nema u bazi!");
                    return;
                }
                int gradId = listaGradId[0];

                var skole = context.Skola.Where(x => x.GradId == gradId)
                .Include(x => x.Grad);

                var data = new StringBuilder();
                foreach (var s in skole)
                {
                    data.AppendLine($"Naziv: {s.Naziv}");
                    data.AppendLine($"Adresa: {s.Adresa}");
                }
                Console.WriteLine(data.ToString());
            }

        }
        private static void DodajGrad(string imeGrada)
        {
            using (var context = new ProbaContext())
            {
                Grad g = new Grad();
                g.Naziv = imeGrada;
                context.Grad.Add(g);
                context.SaveChanges();
            }
        }

        private static void DodajSkolu(string imeSkole, string adresaSkole, int idGrada)
        {
            using (var context = new ProbaContext())
            {
                Skola s = new Skola();
                s.Naziv = imeSkole;
                s.Adresa = adresaSkole;
                s.GradId = idGrada;
                context.Skola.Add(s);
                context.SaveChanges();
            }
        }

    }
}
