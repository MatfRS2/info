using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

//8.
using ProdavnicaKozmetike.Models;

//18.
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ProdavnicaKozmetike
{
    public class Startup
    {
        //18.
         public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //8.
            //services.AddTransient<IProizvodRepozitory, LazniRepozitorijum>();


            //18.
            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(
                    Configuration["Data:ProdavnicaKozmetikeProizvodi:ConnectionString"]));

            services.AddTransient<IProizvodRepozitory, PraviRepozitorijum>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();
            app.UseStaticFiles();

            //13. 
            //app.UseMvc(); - menja se i dodaje se nova default ruta
            app.UseMvc(routes =>
            {
                //31.
                routes.MapRoute(
                  name: null,
                  template: "Proizvod/Page{brojStraneProizvoda}",
                  defaults: new { Controller = "Proizvod", Action = "SpisakProizvoda" }                    
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Proizvod}/{action=SpisakProizvoda}/{id?}");
            });

            SeedData.EnsurePopulated(app);
        }
    }
}
