using AutoMapper;
using Abstracao.Representacao.Usuario.Grupo;
using Abstracao.Representacao.Usuario.Pessoa;

namespace Abstracao.Representacao
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
