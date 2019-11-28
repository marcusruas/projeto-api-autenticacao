using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using System.IO;
using System.Reflection;
using IoC;
using Aplicacao;
using AutoMapper;

namespace Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2", new Info { Title = "PadraoAPI", Version = "v2" });
            });

            //HealthChecks
            services.AddHealthChecks();
            services.AddHealthChecksUI();

            //Cors
            services.AddCors(options => {
                options.AddPolicy("PermissionamentoReact",
                    builder => builder.WithOrigins("http://localhost:3000")
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .AllowAnyOrigin());
            });

            //Injeções de dependência
            IoC.IoC.ConfigurarCamadaRepositorio(services);
            IoC.IoC.ConfigurarCamadaServico(services);

            //Mapeamentos da camada de Aplicacao com Dominio
            var mapeamentoDominio = Mapeamento.PrepararMapeamentoDtoDominio();
            mapeamentoDominio.CompileMappings();
            mapeamentoDominio.AssertConfigurationIsValid();

            services.AddScoped<IMapper>(x => new Mapper(mapeamentoDominio));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "PadraoAPI V2");
            });

            //HealthChecks
            app.UseHealthChecks("/health/json", new HealthCheckOptions() {
                Predicate = _ => true,
                ResponseWriter= UIResponseWriter.WriteHealthCheckUIResponse
            });
            app.UseHealthChecksUI(config => config.UIPath = "/health");

            //Cors
            app.UseCors("PermissionamentoReact");

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
