using Microsoft.Extensions.DependencyInjection;
using Repositorio.Usuario.Interface;
using Repositorio.Usuario.Implementacao;
using Servico.Usuario.Interface;
using Servico.Usuario.Implementacao;

namespace Api.Configuracoes
{
    public static class Dependencias
    {
        public static void ConfigurarInjecoesDependencia(IServiceCollection servicos)
        {
            ConfigurarCamadaRepositorio(servicos);
            ConfigurarCamadaServico(servicos);
        }
        private static void ConfigurarCamadaRepositorio(IServiceCollection servicos)
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
