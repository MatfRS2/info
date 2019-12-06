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

            services.AddTransient<IPorudzbineRepozitorijum, EFPorudzbinaRepozitorijum>();

            /* Ovim kazemo da kada god je potrebna korpa na koji nacin je dobijamo.
             * Zapravo, kreira se instanca SesijaKorpa i ona nad istim Http
             * zahtevima vraca istu korpu.
             */
            services.AddScoped<Korpa>(sp => SesijaKorpa.GetKorpa(sp));

            /* A ovim se kaze da kada nam je potreban IHttpContextAccessor
             * uzimamo HttpContextAccessor.
             * Obezbedjujemo da se svaki put koristi isti objekat.
             */
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc();
            services.AddMemoryCache();
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
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name:null,
                    template: "{trenutnaKategorija}/Strana{tekucaStrana}",
                    defaults: new {Controller = "Proizvod", Action = "Spisak"}
                    );

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
