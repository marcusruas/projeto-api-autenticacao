using Aplicacao.Grupo;
using Dapper;
using MandradePkgs.Conexoes;
using static MandradePkgs.Conexoes.Mapeamentos.DboSqlMapper;
using Repositorio.Grupo.Interface;

namespace Repositorio.Grupo.Implementacao
{
    public class GrupoRep : IGrupoRep
    {
        private IConexaoSQL _conexao { get; set; }

        public GrupoRep(IConexaoSQL conexao) {
            _conexao = conexao;
        }

        public bool AdicionarGrupo(GrupoDbo grupo) {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "insertGrupo", "SHAREDB");
            var parametros = MapearParaDbo(grupo);
            return conexao.Execute(comando, parametros) == 1;
        }
    }
}
