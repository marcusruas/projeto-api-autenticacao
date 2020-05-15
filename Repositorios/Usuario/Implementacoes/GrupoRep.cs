using Dapper;
using MandradePkgs.Conexoes;
using static MandradePkgs.Conexoes.Mapeamentos.DpoSqlMapper;
using Repositorios.Usuario.Interfaces;
using System.Linq;
using Abstracoes.Representacoes.Usuario.Grupo;

namespace Repositorios.Usuario.Implementacoes
{
    public class GrupoRep : IGrupoRep
    {
        private IConexaoSQL _conexao { get; set; }

        public GrupoRep(IConexaoSQL conexao)
        {
            _conexao = conexao;
        }

        public bool AdicionarGrupo(GrupoDpo grupo)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "insertGrupo", "SHAREDB");
            var parametros = DpoParaParametros(grupo);
            return conexao.Execute(comando, parametros) == 1;
        }

        public GrupoDpo ObterGrupoPorId(int id)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "selectGrupoPorId", "SHAREDB");
            var parametros = DpoParaParametros(new { Id = id });
            return conexao.Query<GrupoDpo>(comando, parametros).FirstOrDefault();
        }

        public bool DeletarGrupo(int id)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "deleteGrupo", "SHAREDB");
            var parametros = DpoParaParametros(new { id });
            return conexao.Execute(comando, parametros) == 1;
        }
    }
}
