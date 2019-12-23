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
    /// <summary>
    /// Programski kod koji se izvrsava pri pokretanju
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddScoped<ICategoryRepository, CategoryRepositoryInMemory>();

            services.AddDbContext<KategorijeContext>(options => Configuration.GetConnectionString("KategorijeConnection"));
            services.AddScoped<ICategoryRepository>(
                sp => new CategoryRepositoryEF(
                    sp.GetService<KategorijeContext>())
            );

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
                c.IncludeXmlComments(path.Substring(0, indexComma) + ".xml");
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

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
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
