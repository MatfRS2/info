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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR; // potrebno za SignalR
using Prodavnica.Hubs;              // potrebno za SignalR

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

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(
                    Configuration["Data:ProdavnicaIdentity:ConnectionString"]));
            
            services.AddTransient<IProizvodRepozitorijum, EFRepozitorijum>();
            services.AddTransient<IPorudzbineRepozitorijum, EFPorudzbinaRepozitorijum>();

            services.AddScoped<Korpa>(sp => SesijaKorpa.GetKorpa(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddIdentity<MojKorisnik, IdentityRole>(options => {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzD";
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(opts => opts.LoginPath = "/Account/Prijavljivanje");

            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();

	    /* Dodavanje/konfigurisanje servisa SignalR. */
            services.AddSignalR();
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

            app.UseAuthentication();

            /* Putanje koje koristi SignalR. 
             * Moze ih biti vise --> odredjena klasa ka odredjenoj putanji.
             * Mozemo ih nazvati kako zelimo, ali je neko interno pravilo da se 
             * putanja zove slicno kao i klasa na koju se odnosi.
             * Ova putanja se kasnije koristi u okviru .js fajla.
             */
            app.UseSignalR(routes =>
            {
                routes.MapHub<CetHub>("/cetHub");
            });

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

            
            IdentitySeedData.DodajDefaultRole(app);
        }
    }
}
