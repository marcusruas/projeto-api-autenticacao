using AutoMapper;
using Abstracoes.Representacoes.Usuario.Grupo;
using Microsoft.Extensions.DependencyInjection;

namespace Abstracoes.Representacoes
{
    public static class Mapeamentos
    {
        public static MapperConfiguration DefinirConfiguracoesMapeamento(this IServiceCollection servicos)
        {
            return new MapperConfiguration(cnf =>
            {
            });
        }
    }
}
