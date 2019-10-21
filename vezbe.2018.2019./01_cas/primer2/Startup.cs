using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace primer2
{
    public class Moja_klasa
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        //ovo je priprema (prep envirement)
        public void ConfigureServices(IServiceCollection services)
        {
            // services. -- svasta se moze dodati web strani, cak i 3rd party services (facebook, etc..)
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        //ovo je da se sa onim sto smo pripremili u prethodnoj funkciji urade konkretne stvari
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            /* Da bi pokupio slike, css i ostalo */
            app.UseStaticFiles();

            app.UseMvc();

            /* Ovo mi nece trebati kada dodam app.UseMvc */
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("<b>Zdravo svete</b>");
            });
        }
    }
}
