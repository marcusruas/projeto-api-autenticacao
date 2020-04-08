using Microsoft.Extensions.DependencyInjection;
using Repositorios.Usuario.Interfaces;
using Repositorios.Usuario.Implementacoes;
using Servicos.Usuario.Interfaces;
using Servicos.Usuario.Implementacoes;

namespace Api.Configuracoes
{
    public static class Dependencias
    {
        public static void ConfigurarInjecoesDependencia(IServiceCollection servicos)
        {
            ConfigurarCamadaRepositorios(servicos);
            ConfigurarCamadaServico(servicos);
        }
        private static void ConfigurarCamadaRepositorios(IServiceCollection servicos)
        {
            servicos.AddScoped<IGrupoRep, GrupoRep>();
            servicos.AddScoped<IPessoaRep, PessoaRep>();
        }

        private static void ConfigurarCamadaServico(IServiceCollection servicos)
        {
            servicos.AddScoped<IGrupoSrv, GrupoSrv>();
            servicos.AddScoped<IPessoaSrv, PessoaSrv>();
        }
    }
}
