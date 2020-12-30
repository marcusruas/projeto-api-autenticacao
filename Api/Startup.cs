using Api.Configuracoes;
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
            Middlewares Configuracoes = new Middlewares(services, Configuration, GetType());
            Configuracoes.ConfigurarServicos();
            ConfigurarInjecoesDependencia(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Implementacoes Configuracoes = new Implementacoes(app, env);
            Configuracoes.ConfigurarAplicacao();
        }
    }
}
