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
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePages();
            app.UseStaticFiles();

            //menjanje i poboljsavanje ruta
            app.UseMvc(routes =>
            {
                /* Zadato je da ruta ima izgled: /Prozivodi/Strana1 ili /Prozivodi/Strana3 itd...
                 * 
                */
                routes.MapRoute(
                    name: null, // moze da se stavi "prikazspiskova" i onda se ovo ime moze navesti u kontroleru kad zelimo bas takvu rutu
                    template: "Proizvodi/Strana{tekucaStrana}",
                    defaults: new {Controller = "Proizvod", Action = "Spisak"}
                    );

                routes.MapRoute(
                    name:default,
                    template: "{controller=Proizvod}/{action=Spisak}/{id?}");
            });
        }
    }
}
