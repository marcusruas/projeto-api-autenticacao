using Api.Configuracoes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            Servicos Configuracoes = new Servicos(services, GetType());
            Configuracoes.ConfigurarServicos();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            Implementacoes Configuracoes = new Implementacoes(app, env);
            Configuracoes.ConfigurarAplicacao();
        }
    }
}
