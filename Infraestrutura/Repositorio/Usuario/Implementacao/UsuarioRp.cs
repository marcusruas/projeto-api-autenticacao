using System.Linq;
using Dapper;
using Infraestrutura.Repositorio.Entidade;
using Infraestrutura.Repositorio.Usuario.Entidade;
using MandradePkgs.Conexoes;
using Infraestrutura.Repositorio.Usuario.Interface;
using static MandradePkgs.Conexoes.Estrutura.Mapeamento.DpoSqlMapper;
using MandradePkgs.Conexoes.Estrutura.Model;

namespace Infraestrutura.Repositorio.Usuario.Implementacao
{
    public class UsuarioRp : ClasseRepositorio, IUsuarioRp
    {
        private IConexaoSQL _conexao { get; }

        public UsuarioRp(IConexaoSQL conexao) : base("Usuario")
        {
            _conexao = conexao;
        }

        public bool InserirUsuario(UsuarioDpo usuario)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "insertUsuario", "SHAREDB");
            var parametros = DpoParaParametros(usuario);
            return conexao.Execute(comando, parametros) == 1;
        }

        public PessoaDpo BuscarPessoaUsuario(int id)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "selectPessoaUsuario", "SHAREDB");
            return conexao.QueryFirstOrDefault<PessoaDpo>(comando, new { Id = id });
        }

        public GrupoDpo BuscarGrupoUsuario(int id)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "selectGrupoUsuario", "SHAREDB");
            return conexao.QueryFirstOrDefault<GrupoDpo>(comando, new { Id = id });
        }

        public (UsuarioDpo, GrupoDpo, PessoaDpo) BuscarUsuario(string usuario, string senha)
        {
            UsuarioDpo usuarioBanco;
            GrupoDpo grupoBanco;
            PessoaDpo pessoaBanco;
            object parametros = new { usuario, senha };

            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "selectUsuario", "SHAREDB");
            using (var retorno = conexao.QueryMultiple(comando, parametros))
            {
                usuarioBanco = retorno.Read<UsuarioDpo>().First();
                grupoBanco = retorno.Read<GrupoDpo>().First();
                pessoaBanco = retorno.Read<PessoaDpo>().First();
            }

            if(usuarioBanco == null) {
                return (null, null, null);
            }

            return (usuarioBanco, grupoBanco, pessoaBanco);
        }

        public bool AtualizarAtivoUsuario(int id, bool ativo)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "updateAtivoUsuario", "SHAREDB");
            var parametros = new { id, ativo };
            return conexao.Execute(comando, parametros) == 1;
        }

        public bool AtualizarSenhaUsuario(int id, string senhaAntiga, string senhaNova)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "updateSenhaUsuario", "SHAREDB");
            var parametros = new
            {
                Id = id,
                SenhaAntiga = senhaAntiga,
                senhaNova = senhaNova
            };
            return conexao.Execute(comando, parametros) == 1;
        }

        public (UsuarioDpo, GrupoDpo, PessoaDpo) BuscarUsuario(int usuario)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "selectUsuarioPorId", "SHAREDB");
            UsuarioDpo usuarioBanco;
            GrupoDpo grupoBanco;
            PessoaDpo pessoaBanco;
            using (var retorno = conexao.QueryMultiple(
                comando,
                new { Usuario = usuario })
            )
            {
                usuarioBanco = retorno.Read<UsuarioDpo>().First();
                grupoBanco = retorno.Read<GrupoDpo>().First();
                pessoaBanco = retorno.Read<PessoaDpo>().First();
            }

            if(usuarioBanco == null) {
                return (null, null, null);
            }

            return (usuarioBanco, grupoBanco, pessoaBanco);
        }
    }
}