using System;
using System.Text;

namespace HelloConsole
{
    public static class Prosirenje
    {
        public static int BrojSlova(this string s)
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

        public static string Nalepi(this string s, int brojPonavljanja)
        {
            if (brojPonavljanja <= 1)
                return s;
            StringBuilder graditelj = new StringBuilder(s);
            for (int i = 1; i <= brojPonavljanja; i++)
            {
                graditelj.Append(s);
            }
            return graditelj.ToString();
        }
    }
}
