using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.IO;
using System.Linq;
using System.Reflection;

using Swashbuckle.AspNetCore.Swagger;

namespace DIinCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "RS2 API",
                    Description = "Restfull servis koji podržava rad sistema.",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Vladimir Filipovic",
                        Email = "vladofilipovic@hotmail.com",
                        Url = "http://www.math.rs/~vladaf"
                    },
                    License = new License
                    {
                        Name = "Use under Matf RS2 license",
                        Url = "http://www.math.rs/~vladaf/MatfRS2/License"
                    }
                });
                Assembly currentAssembly = Assembly.GetExecutingAssembly();
                string path = currentAssembly.GetName().ToString();
                int indexComma = path.IndexOf(',');
                c.IncludeXmlComments( path.Substring(0, indexComma) + ".xml");
                string[] xmlDocs = currentAssembly.GetReferencedAssemblies()
                    .Union(new AssemblyName[] { currentAssembly.GetName() })
                    .Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location), $"{a.Name}.xml"))
                    .Where(f => File.Exists(f)).ToArray();
                Array.ForEach(xmlDocs, (d) =>
                {
                    c.IncludeXmlComments(d);
                });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Olimp API V1");
                c.OAuthClientId("olimpCrudServiceSwagger");
                c.OAuthClientSecret("");
                c.OAuthRealm("");
                c.OAuthAppName("Swagger UI");
            });
        }
    }
}
