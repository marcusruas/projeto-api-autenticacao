using Aplicacao;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using MandradePkgs.Conexoes.Configuracao;
using MandradePkgs.Retornos.Configuracao;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc(
                cnf => cnf.ImplementarFiltrosRetorno()
            ).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Implementando conexões de BD
            services.ImplementarConexaoSQL(GetType());
            //Implementando Mensageria
            services.ImplementarMensagensRetorno();

            //Swagger
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v2", new Info { Title = "PadraoAPI", Version = "v2" });
            });

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

        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseHsts();
            }

            //Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "PadraoAPI V2");
            });

            //Cors
            app.UseCors("PermissionamentoReact");

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
