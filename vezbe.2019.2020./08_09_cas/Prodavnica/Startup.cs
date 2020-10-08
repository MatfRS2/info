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

            /* Podesavamo Entity Framework Core koji omogucava MVCaplikaciji
             * servise za pristup podacima.
             * Metod AddDbContext dodaje servise neophodne za rad Entity Framework Core.
             * Metod UseSqlServer dodaje podrsku neophodnu za cuvanje podataka 
             * koriscenjem Microsoft SQL Server.
             * Podrazumevana konfiguracija: string za povezivanje sa bazom je u appsettings.json
             * (moze se menjati).
             */
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(
                    Configuration["Data:ProdavnicaIdentity:ConnectionString"]));
            
            services.AddTransient<IProizvodRepozitorijum, EFRepozitorijum>();
            services.AddTransient<IPorudzbineRepozitorijum, EFPorudzbinaRepozitorijum>();

            services.AddScoped<Korpa>(sp => SesijaKorpa.GetKorpa(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            /* Podesavanje servisa za koriscenje ASP.NET Core Identity.
             * Metod AddIdentity ima dva tip-parametra kojim se odredjuje 
             * koja klasa reprezentuje korisnika (to je klasa MojKorisnik koju smo 
             * mi napravili) i koja klasa sluzi za reprezentovanje
             * uloga (koristimo ugradjenu klasu IdentityRole, ali smo mogli napraviti 
             * i svoju klasu koja nasledjuje IdentityRole i dodati jos jos neke
             * funkcionalnosti ukoliko je potrebno).
             * 
             * Sa AddEntityFrameworkStores se specifikuje da ASP.NET Core Identity
             * koristi Entity Framework Core za cuvanje i citanje podataka.
             * Entity Framework Core je podesen par redova iznad.
             * 
             * Metod AddDefaultTokenProviders sluzi da se zada podrazumevana
             * konfiguracija za rad sa tokenima. Menjanje sifri (recimo)
             * zahteva rad sa tokenima pa je zato neophodno ovo postaviti.
             * 
             * Mogu se zadati neki zahtevi za sifru.
             * Za nesugradjene opcije za sifru potrebno je napraviti klasu
             * koja nasledjuje  IPasswordValidator<MojKorisnik>.
             * U toj klasi potrebno je napisati metod
             *  Task<IdentityResult> ValidateAsync(...) ...
             *  
             *  Slicno je i za opcije sa korisnickim imenom
             *  (potrebno je napraviti klasu koja nasledjuje  IUserValidator<AppUser> ....).
             */
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

            /* Konfigurisanje putanje ka Log in strani.
             */ 
            services.ConfigureApplicationCookie(opts => opts.LoginPath = "/Account/Prijavljivanje");

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

            /* Korisnicki podaci vezani za zahteve koji dolaze od korisnika
             * su zasnovani na kolacicima ili prezapisivanju URL-a.
             * To znaci da detalji korisnickih naloga nisu direktno ukljuceni u
             * Http zahteve koji se salju aplikaciji i nisu
             * direktno ukljuceni u odgovore koje aplikacija salje.
             */
            app.UseAuthentication();

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

            /* Prilikom pokretanja aplikacije prave se role.
             * Ovo ce se desiti samo jednom ako te role ne postoje.
             */
            IdentitySeedData.DodajDefaultRole(app);
        }
    }
}
