
using System;
using System.Text;

namespace HelloConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            prikazDo42();
            int z = 2;
            StampajViseLinija(z);
            Console.WriteLine("Hello World!");
            StampajViseLinija(z);
            z = BrojSlova("123 Miki Maus 123");
            Console.WriteLine(z);
            Console.WriteLine(Nalepi("Miki Maus ", 2));
            Console.WriteLine(Nalepi("Miki Maus ", 4));
        }

        static void prikazDo42()
        {
            int i = 40;
            while (i <= 42)
            {
                Console.WriteLine(i);
                i++;
            }
        }

        static void StampajViseLinija(int z)
        {
            for (int i = 0; i < z; i++)
                Console.WriteLine("-- " + i + " --");
        }

        static int BrojSlova(string s)
        {
            int ret = 0;
            foreach (char ch in s)
            {
                if (ch >= 'a' && ch <= 'z')
                    ret++;
                if (ch >= 'A' && ch <= 'Z')
                    ret++;
            }
            return ret;
        }

        static string Nalepi(string s, int brojPonavljanja)
        {
            if (brojPonavljanja <= 1)
                return s;
            StringBuilder graditelj = new StringBuilder(s);
            for(int i = 1; i<=brojPonavljanja; i++)
            {
                graditelj.Append(s);
            }
            return graditelj.ToString();
        }

    }
}
