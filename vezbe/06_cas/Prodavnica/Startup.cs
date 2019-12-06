using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Prodavnica.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Prodavnica
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; set; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContex>(options =>
                options.UseSqlServer(
                    Configuration["Data:ProdavnicaProizvodi:ConnectionString"]));
            services.AddTransient<IProizvodRepozitorijum, EFRepozitorijum>();
            
            services.AddMvc();

            /* Ovim omogucavamo da se deo podataka zapise u RAM-u u lokalu
             * a ne da se uvek prenosi preko networka do servera (jer je to sporije).
             */
            services.AddMemoryCache();

            /* Omogucavamo rad sa sesijama. */
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePages();
            app.UseStaticFiles();
            /* Omogucavamo rad sa sesijama. */
            app.UseSession();

            //menjanje i poboljsavanje ruta
            app.UseMvc(routes =>
            {
                //menjamo rute da bi mogle da se prikazu i kategorije
                //ova ruta je najduza (imamo i kategoriju i stranu)
                //ruta oblika .../kategorija/StranaBr
                //npr. .../racunari/Strana3
                routes.MapRoute(
                    name:null,
                    template: "{trenutnaKategorija}/Strana{tekucaStrana}",
                    defaults: new {Controller = "Proizvod", Action = "Spisak"}
                    );

                /* Rute navodimo u redosledu od najduze rute ka najkracoj ruti.
                 * Naredne dve rute su iste duzine: mozemo imati samo kategoriju ili samo tekucu stranu (npr. ../kategorija ili ../Strana3)
                 * Ipak, u ovom primeru dolazi do problema jer se moze desiti da se kategorija unifikuje sa Strana3 (i pokusava da nadje kategoriju Strana3)
                 * U ovom nasem primeru najlakse je zameniti ove dve naredne rute:
                 * 
                 * template:"{trenutnaKategorija}" i template:"Strana{tekucaStrana},
                 * tj. zameniti im redosled.
                 * Ipak ako ostavimo ovako kako jeste, onda se 
                 * trenutnaKategorija unifikuje sa Strana1 (ili Strana2, Strana3...)
                 * i pokusava da ispise sve proizvode koji su kategorije "Strana1".
                 * To nije ponasanje koje zelimo.
                 * Stoga, uvodimo ovo "constraints". Prosledjujemo regular izraz
                 * i kazemo da trenutnaKategorija ne treba da bude jednaka StranaBrojStaGod.
                 * Za "constraints" mozemo pisati i svoje klase i tako praviti
                 * nasa ogranicenja kakva god zelimo.
                 */
                routes.MapRoute(
                    name:null,
                    template:"{trenutnaKategorija}",
                    defaults: new { Controller = "Proizvod", Action = "Spisak", tekucaStrana = 1},
                    constraints: new { trenutnaKategorija = @"?!(Strana[0-9]*)" }
                    );

                routes.MapRoute(
                    name:null,
                    template:"Strana{tekucaStrana}",
                    defaults: new { Controller = "Proizvod", Action = "Spisak", trenutnaKategorija = "" }
                    );

                routes.MapRoute(
                    name:null,
                    template:"",
                    defaults: new { Controller = "Proizvod", Action = "Spisak", tekucaStrana = 1 }
                    );

                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}"
                    );
            });
        }
    }
}
