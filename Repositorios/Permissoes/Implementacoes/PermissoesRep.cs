using Abstracoes.Representacoes.Permissoes.Permissao;
using Dapper;
using MandradePkgs.Conexoes;
using Repositorios.Permissoes.Interfaces;
using static MandradePkgs.Conexoes.Mapeamentos.DpoSqlMapper;

namespace Repositorios.Permissoes.Implementacoes
{
    public class PermissoesRep : IPermissoesRep
    {
        private IConexaoSQL _conexao;

        public PermissoesRep(IConexaoSQL conexao)
        {
            _conexao = conexao;
        }

        public bool InserirPermissao(PermissaoDpo permissao)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "insertPermissao", "SHAREDB");
            var parametros = DpoParaParametros(permissao);
            return conexao.Execute(comando, parametros) == 1;
        }
    }
}