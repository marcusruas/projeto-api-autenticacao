using Aplicacao.Pessoa;
using Dapper;
using MandradePkgs.Conexoes;
using static MandradePkgs.Conexoes.Mapeamentos.DboSqlMapper;
using Repositorio.Pessoa.Interface;

namespace Repositorio.Pessoa.Implementacao
{
    public class PessoaRep : IPessoaRep
    {
        private IConexaoSQL _conexao { get; }

        public PessoaRep(IConexaoSQL conexao) {
            _conexao = conexao;
        }

        public bool InserirPessoa(PessoaDbo pessoa) {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "insertPessoa", "SHAREDB");
            var parametros = MapearParaDbo(pessoa);
            return conexao.Execute(comando, parametros) == 1;
        }
    }
}
