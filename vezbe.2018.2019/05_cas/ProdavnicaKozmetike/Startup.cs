using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ProdavnicaKozmetike.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;


namespace ProdavnicaKozmetike
{
    public class Startup
    {
         public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(
                    Configuration["Data:ProdavnicaKozmetikeProizvodi:ConnectionString"]));

            services.AddTransient<IProizvodRepozitory, PraviRepozitorijum>();

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

            //2.
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: null,
                    template: "{kategorija}/Strana{brojStraneProizvoda:int}",
                    defaults: new { Controller = "Proizvod", Action = "SpisakProizvoda", brojStraneProizvoda = 1}
                );

                routes.MapRoute(
                  name: null,
                  template: "Strana{brojStraneProizvoda:int}",
                  defaults: new { Controller = "Proizvod", Action = "SpisakProizvoda" }                    
                );

                routes.MapRoute(
                    name: null,
                    template: "{kategorija}",
                    defaults: new { Controller = "Proizvod", Action = "SpisakProizvoda", brojStraneProizvoda = 1}
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Proizvod}/{action=SpisakProizvoda}/{id?}");
            });

            SeedData.EnsurePopulated(app);
        }
    }
}
