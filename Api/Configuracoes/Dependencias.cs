using Microsoft.Extensions.DependencyInjection;
using Repositorio.Grupo.Interface;
using Repositorio.Grupo.Implementacao;
using Servico.Grupo.Interface;
using Servico.Grupo.Implementacao;
using Repositorio.Pessoa.Interface;
using Repositorio.Pessoa.Implementacao;
using Servico.Pessoa.Interface;
using Servico.Pessoa.Implementacao;

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
