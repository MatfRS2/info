
using System;

namespace HelloConsole
{
    partial class Program
    {
        static void StampajViseLinija(int z)
        {
            for (int i = 0; i < z; i++)
                Console.WriteLine("-- " + i + " --");
        }

        static void Main(string[] args)
        {
            int z = 2;
            StampajViseLinija(z);
            Console.WriteLine("Hello World!");
            StampajViseLinija(z);

            //z = Prosirenje.BrojSlova("123 Miki Maus 123");
            //Console.WriteLine(z);
            //Console.WriteLine(Prosirenje.Nalepi("Miki Maus ", 2));
            //Console.WriteLine(Prosirenje.Nalepi("Miki Maus ", 4));

            z = "123 Miki Maus 123".BrojSlova();
            Console.WriteLine(z);
            Console.WriteLine("Miki Maus ".Nalepi(2));
            Console.WriteLine("Miki Maus ".Nalepi(4));
        }

    }
}
