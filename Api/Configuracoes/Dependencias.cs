using Microsoft.Extensions.DependencyInjection;
using Repositorios.Usuario.Interfaces;
using Repositorios.Usuario.Implementacoes;
using Servicos.Usuario.Interfaces;
using Servicos.Usuario.Implementacoes;
using Servicos.Permissoes.Interfaces;
using Servicos.Permissoes.Implementacoes;
using Repositorios.Permissoes.Implementacoes;
using Repositorios.Permissoes.Interfaces;

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
            servicos.AddScoped<IUsuarioRep, UsuarioRep>();
        }

        private static void ConfigurarCamadaServico(IServiceCollection servicos)
        {
            servicos.AddScoped<IGrupoSrv, GrupoSrv>();
            servicos.AddScoped<IPessoaSrv, PessoaSrv>();
            servicos.AddScoped<IUsuarioSrv, UsuarioSrv>();
            servicos.AddScoped<IPermissoesSrv, PermissoesSrv>();
            servicos.AddScoped<IPermissoesRep, PermissoesRep>();
        }
    }
}
