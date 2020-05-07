using Abstracoes.Representacoes.Usuario.Usuario;
using Dapper;
using MandradePkgs.Conexoes;
using Repositorios.Usuario.Interfaces;
using static MandradePkgs.Conexoes.Mapeamentos.DpoSqlMapper;

namespace Repositorios.Usuario.Implementacoes
{
    public class UsuarioRep : IUsuarioRep
    {
        private IConexaoSQL _conexao { get; }

        public UsuarioRep(IConexaoSQL conexao)
        {
            _conexao = conexao;
        }

        public bool InserirUsuario(UsuarioDpo usuario)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "insertUsuario", "SHAREDB");
            var parametros = DpoParaParametros(usuario);
            return conexao.Execute(comando, parametros) == 1;
        }
    }
}