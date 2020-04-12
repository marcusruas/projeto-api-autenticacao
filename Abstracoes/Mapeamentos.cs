using Abstracoes.Tradutores.Usuario.Implementacoes;
using Abstracoes.Tradutores.Usuario.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Abstracoes
{
    public static class Mapeamentos
    {
        public static void DefinirConfiguracoesMapeamento(this IServiceCollection servicos)
        {
            servicos.AddScoped<IPessoaTrd, PessoaTrd>();
        }
    }
}