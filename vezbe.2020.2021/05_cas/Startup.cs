using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prodavnica2.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.AspNetCore.Mvc;

namespace Prodavnica2
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration["Data:ProdavnicaProizvodi:ConnectionString"]));
            //services.AddTransient<IProizvodRepozitorijum, LazniRepozitorijum>();
            services.AddTransient<IProizvodRepozitorijum, EFRepozotorijum>();
            services.AddTransient<IPorudzbineRepozitorijum, EFPorudzbinaRepozitorijum>();

            services.AddMemoryCache();
            services.AddSession();

            services.AddScoped<Korpa>(sp => SesijaKorpa.GetKorpa(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePages();
            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: null,
                    "{kategorija}/Strana{Strana:int}",
                    new { Controller = "Proizvod", Action = "Spisak", Strana = 1}
                    );

                endpoints.MapControllerRoute(
                    name: null,
                    "{kategorija}",
                    new { Controller = "Proizvod", Action = "Spisak", Strana = 1 },
                    constraints: new
                    {
                        kategorija = @"?!(Strana[0-9]*)"
                    }
                    );

                endpoints.MapControllerRoute(
                    name: null,
                    "Strana{Strana}",
                    new { Controller = "Proizvod", Action = "Spisak" }
                    );

                endpoints.MapControllerRoute(name: "default",
                    pattern: "{controller=Proizvod}/{action=Spisak}/{id?}");
            });
        }
    }
}

