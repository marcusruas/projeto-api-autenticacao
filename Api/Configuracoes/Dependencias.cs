using Microsoft.Extensions.DependencyInjection;
using Infraestrutura.Repositorio.Usuario.Interface;
using Infraestrutura.Repositorio.Usuario.Implementacao;
using Infraestrutura.Servico.Usuario.Interface;
using Infraestrutura.Servico.Usuario.Implementacao;
using Infraestrutura.Servico.Permissao.Interface;
using Infraestrutura.Servico.Permissao.Implementacao;
using Infraestrutura.Repositorio.Permissao.Interface;
using Infraestrutura.Repositorio.Permissao.Implementacao;

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
            servicos.AddScoped<IGrupoRp, GrupoRp>();
            servicos.AddScoped<IPessoaRp, PessoaRp>();
            servicos.AddScoped<IUsuarioRp, UsuarioRp>();
            servicos.AddScoped<IPermissaoRp, PermissaoRp>();
        }

        private static void ConfigurarCamadaServico(IServiceCollection servicos)
        {
            servicos.AddScoped<IGrupoSv, GrupoSv>();
            servicos.AddScoped<IPessoaSv, PessoaSv>();
            servicos.AddScoped<IUsuarioSv, UsuarioSv>();
            servicos.AddScoped<IPermissaoSv, PermissaoSv>();
        }
    }
}
