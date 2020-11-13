using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prodavnica2.Infrastructure;
using Newtonsoft.Json;

namespace Prodavnica2.Models
{
    public class SesijaKorpa : Korpa
    {
        public static Korpa GetKorpa(IServiceProvider service)
        {
            ISession sesija = service.GetRequiredService<IHttpContextAccessor>()
                ?.HttpContext.Session;

           SesijaKorpa korpa = sesija.GetJson<SesijaKorpa>("Korpa") ?? 
                new SesijaKorpa();

            korpa.Session = sesija;

            return korpa;
        }

        [JsonIgnore]
        public ISession Session { get; set; }

        public override void DodajProizvod(Proizvod proizvod, int kolicina)
        {
            base.DodajProizvod(proizvod, kolicina);
            Session.SetJson("Korpa", this);
        }

        public override void ObrisiProizvod(Proizvod proizvod)
        {
            base.ObrisiProizvod(proizvod);
            Session.SetJson("Korpa", this);
        }

        public override void ObrisiKorpu()
        {
            base.ObrisiKorpu();
            Session.Remove("Korpa");
        }
    }
}
