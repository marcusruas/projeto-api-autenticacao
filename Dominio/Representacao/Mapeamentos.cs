using AutoMapper;
using static Dominio.Representacao.Grupo.GrupoMap;
using static Dominio.Representacao.Pessoa.PessoaMap;

namespace Dominio.Representacao
{
    public static class Mapeamentos
    {
        public static MapperConfiguration DefinirConfiguracoesMapeamento()
        {
            return new MapperConfiguration(cnf =>
            {
                cnf.ConfigurarMapeamentosGrupo();
                cnf.ConfigurarMapeamentosPessoa();
            });
        }
    }
}
