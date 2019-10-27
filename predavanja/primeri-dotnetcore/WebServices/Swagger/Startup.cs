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
                var currentAssembly = Assembly.GetExecutingAssembly();
                var xmlDocs = currentAssembly.GetReferencedAssemblies()
                    .Union(new AssemblyName[] { currentAssembly.GetName() })
                    .Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location), $"{a.Name}.xml"))
                    .Where(f => File.Exists(f)).ToArray();
                Array.ForEach(xmlDocs, (d) =>
                {
                    c.IncludeXmlComments(d);
                });
                c.TagActionsBy(x =>
                    {
                        string[] s = x.RelativePath.Split('/');
                        if (s.Length >= 4)
                            return s[0] + "/" + s[1] + "/" + s[2] + "/" + s[3];
                        else if (s.Length >= 3)
                            return s[0] + "/" + s[1] + "/" + s[2];
                        else if (s.Length >= 2)
                            return s[0] + "/" + s[1];
                        else if (s.Length >= 1)
                            return s[0];
                        else
                            return "";
                    }
                 );
                c.OrderActionsBy(x =>
                {
                    string[] s = x.RelativePath.Split('/');
                    if (s.Length >= 4)
                        return s[0] + "/" + s[1] + "/" + s[2] + "/" + s[3];
                    else if (s.Length >= 3)
                        return s[0] + "/" + s[1] + "/" + s[2];
                    else if (s.Length >= 2)
                        return s[0] + "/" + s[1];
                    else if (s.Length >= 1)
                        return s[0];
                    else
                        return "";
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
