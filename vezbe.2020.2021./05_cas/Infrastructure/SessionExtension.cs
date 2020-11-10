using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodavnica2.Infrastructure
{
    public static class SessionExtension
    {
        public static void SetJson(this ISession sesija, string key, object value)
        {
            sesija.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetJson<T>(this ISession sesija, string key)
        {
            var sesijaPodaci = sesija.GetString(key);

            return sesijaPodaci == null ?
                default(T) : JsonConvert.DeserializeObject<T>(sesijaPodaci);
        }
    }
}
