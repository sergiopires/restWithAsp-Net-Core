using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using restWithAspNetCore.Model.Context;
using restWithAspNetCore.Businnes.Implementations;
using restWithAspNetCore.Repository.Implementations;
using restWithAspNetCore.Repository.Generic;
using Microsoft.Net.Http.Headers;
using Tapioca.HATEOAS;
using restWithAspNetCore.Hypermedia;
using Microsoft.AspNetCore.Rewrite;

namespace restWithAspNetCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Add banco
            var connection = Configuration["MySqlConnectionString:MySqlConnectionString"];

            services.AddDbContext<MySQLContext>(options => options.UseMySql(connection));

            //Se algum cliente antigo precisar de um formato xml
            services.AddMvc(
               options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml",
                    MediaTypeHeaderValue.Parse("txt/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json",
                    MediaTypeHeaderValue.Parse("application/json"));
            }).AddXmlSerializerFormatters();

            //Adicionando HATEOAS Hypermedia no startup
            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ObjectContentResponseEnricherList.Add(new PersonEnricher());

            services.AddSingleton(filterOptions);

            //Swagger documentacao da api
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "RESTful API with ASP.NET Core 2.0",
                        Version = "V1"
                    });
            });


            //Add versionador
            services.AddApiVersioning();

            //Injecao de dependencias
            services.AddScoped<IPersonBusinnes, PersonBusinnesImpl>();
            services.AddScoped<IBookBusinnes, BookBusinnesImpl>();
            services.AddScoped<IPersonRepository, PersonRepository>();

            services.AddScoped<IBookBusinnes, BookBusinnesImpl>();
            services.AddScoped<IBookRepository, BookRepository>();

            //repositorio generico
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI( c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "DefaultApi",
                    template: "{controller=Values}/{id?}"
                    );
            });
        }
    }
}
