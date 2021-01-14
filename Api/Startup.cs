using Api.Configuracoes;
using MandradePkgs.ConfiguracaoAPI.Configuracao;
using MandradePkgs.ConfiguracaoAPI.Estrutura.Modelos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static Api.Configuracoes.Dependencias;

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
            ConfigurarInjecoesDependencia(services);
            ConfiguracaoAPI.ImplementarConfiguracoesServicos(services);
            ConfiguracaoAPI.ImplementarMandradePKGS(services, Configuration, GetType());
            services.AddMvc(cnf => ConfiguracaoAPI.ImplementarConfiguracoesMvc(cnf));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            SwaggerParametros parametros = new SwaggerParametros("UsuarioAPI", "V1");
            ConfiguracaoAPI.ImplementarConfiguracoesMiddlewares(app, parametros);
        }
    }
}
