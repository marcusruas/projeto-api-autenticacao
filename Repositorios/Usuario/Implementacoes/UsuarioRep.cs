using System.Linq;
using Abstracoes.Representacoes.Usuario.Grupo;
using Abstracoes.Representacoes.Usuario.Pessoa;
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

        public PessoaDpo BuscarPessoaUsuario(int id)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "selectPessoaUsuario", "SHAREDB");
            return conexao.QueryFirstOrDefault<PessoaDpo>(comando, new { Id = id });
        }

        public GrupoDpo BuscarGrupoUsuario(int id)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "selectGrupoUsuario", "SHAREDB");
            return conexao.QueryFirstOrDefault<GrupoDpo>(comando, new { Id = id });
        }

        public (UsuarioDpo, GrupoDpo, PessoaDpo) BuscarUsuario(string usuario, string senha)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "selectUsuario", "SHAREDB");
            UsuarioDpo usuarioBanco;
            GrupoDpo grupoBanco;
            PessoaDpo pessoaBanco;
            using (var retorno = conexao.QueryMultiple(
                comando,
                new
                {
                    Usuario = usuario,
                    Senha = senha
                })
            )
            {
                usuarioBanco = retorno.Read<UsuarioDpo>().First();
                grupoBanco = retorno.Read<GrupoDpo>().First();
                pessoaBanco = retorno.Read<PessoaDpo>().First();
            }

            return (usuarioBanco, grupoBanco, pessoaBanco);
        }
    }
}