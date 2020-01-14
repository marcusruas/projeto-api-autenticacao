using Aplicacao.Grupo;
using Dapper.FluentMap;
using Dapper.FluentMap.Configuration;

namespace Aplicacao
{
    public static class Mapeamentos
    {
        public static void MapearObjetosBanco(this FluentMapConfiguration configuracoes) {
            configuracoes.AddMap(new DboMapper<GrupoDbo>());
        }
    }
}
