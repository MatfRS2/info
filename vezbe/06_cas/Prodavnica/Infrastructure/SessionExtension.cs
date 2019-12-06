using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Prodavnica.Infrastructure
{
    public static class SessionExtension
    {
        /* Da bi mogli da procitamo i zapamtimo Korpu pravimo svoja dva metoda
         * jer sesije nude samo rad sa osnovnim tipovima: char, bool, int (a Korpa to nije).
         * Da bi lako zapamtili podatke za datu sesiju pamtimo ih u stringu
         * pri cemu je taj string specijalan.
         * Korisitmo postojece metode JsonConvert.(De)SerializeObject
         * za citanje objekta iz stringa ili za pamcenje objekata na pametan nacin u string.
         */
        public static void SetJson(this ISession sesija, string key, object value)
        {
            sesija.SetString(key, JsonConvert.SerializeObject(value));
        }

        /* default(T) vraca defaultnu vrednost za dati tip.
         * Ako je T kalsa vratice null.
         * Ako je bool -- false
         * Za numericke vrednosti 0 itd.
         */
        public static T GetJson<T>(this ISession sesija, string key)
        {
            var sesijaPodaci = sesija.GetString(key);

            return sesijaPodaci == null ? default(T) :
                JsonConvert.DeserializeObject<T>(sesijaPodaci);
        }
    }
}
