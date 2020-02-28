using AutoMapper;
using Dominio.Representacao.Usuario.Grupo;
using Dominio.Representacao.Usuario.Pessoa;

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
