using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http; //zbog ISession
using Microsoft.Extensions.DependencyInjection; //zbog poziva GetRequiredService
using Prodavnica.Infrastructure; // zbog poziva .GetJson<Korpa>
using Newtonsoft.Json; //zbog onog [Json ignore...]

namespace Prodavnica.Models
{
    public class SesijaKorpa : Korpa
    {
        public static Korpa GetKorpa(IServiceProvider service)
        {
            ISession sesija = service.GetRequiredService<IHttpContextAccessor>()
                ?.HttpContext.Session;

            SesijaKorpa korpa = sesija?.GetJson<SesijaKorpa>("Korpa")
                ?? new SesijaKorpa();

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

        public override void ObrisiProizvod(Proizvod proizvod, int kolicina)
        {
            base.ObrisiProizvod(proizvod, kolicina);
            Session.SetJson("Korpa", this);
        }

        public override void ObrisiKorpu()
        {
            base.ObrisiKorpu();
            Session.Remove("Korpa");
        }
    }
}
