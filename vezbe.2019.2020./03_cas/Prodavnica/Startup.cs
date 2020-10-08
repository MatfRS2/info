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
        /* Prilikom keriranja instance klase Startup u Program.cs
         * (kad pozivamo CreateDefaultBuilder)
         * zadaju se i parametri za konfiguraciju
         * i tom prilikom se kaze da je konfiguracija smestena u appsetting.json.
         * Ova default konfiguracija se moze promeniti, ali mi to ne radimo ovde.
         * Da bi mogli da korisitimo opcije koje nudi konfiguracija potrebno je
         * konstruktoru Startup klase proslediti konfiguraciju.
         */
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Ovim kazemo da svaki put kada se koristi IProizvodRepozitorijum
            //kreira se novi LazniRepozitorijum objekat
            //Prvo probati sa ovim, a onda preci na konfiguraciju sa Bazom 
            //services.AddTransient<IProizvodRepozitorijum, LazniRepozitorijum>();

            //konfiguracija za bazu
            services.AddDbContext<ApplicationDbContex>(options =>
                options.UseSqlServer(
                    Configuration["Data:ProdavnicaProizvodi:ConnectionString"]));
            services.AddTransient<IProizvodRepozitorijum, EFRepozitorijum>();
            
            //ovde se dodaju objekti koji su zajednicki u aplikaciji
            //AddMvc omogucava koriscenje odredjenih zajednickih objekata u aplikaciji
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            /* Komponente ispod se nazivaju jos i middleware.
             * Redosled je bitan.
             * Vise procitati u Readme fajlu ili (naravno :)) u knjizi, interentu itd.
             */
            //korisno tokom razvoja aplikacije da bi lakse prepoznali greske
            //kada se aplikacija pusti u rad ovu opciju iskljuciti
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //dodaje jednostavne poruke, npr. 404 Page not found
            app.UseStatusCodePages();
            //omogucava koriscenje statickog sadrzaja iz wwwroot foldera
            app.UseStaticFiles();
            //omogucava rad ASP.NET Core MVC
            //ne koristimo default rutu (Home/Index) vec pravimo svoju koriscenjem lambda izraza
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name:default,
                    template: "{controller=Proizvod}/{action=Spisak}/{id?}");
            });
        }
    }
}
