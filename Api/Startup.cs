using Api.Configuracoes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            Middlewares Configuracoes = new Middlewares(services, GetType());
            Configuracoes.ConfigurarServicos();
            ConfigurarInjecoesDependencia(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Implementacoes Configuracoes = new Implementacoes(app, env);
            Configuracoes.ConfigurarAplicacao();
        }
    }
}
