using Aplicacao.Grupo;
using Dapper;
using MandradePkgs.Conexoes;
using static MandradePkgs.Conexoes.Mapeamentos.DboSqlMapper;
using Repositorio.Grupo.Interface;
using System.Collections.Generic;
using System.Linq;

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
            var parametros = DboParaParametros(grupo);
            return conexao.Execute(comando, parametros) == 1;
        }

        public List<GrupoDbo> ObterGruposPorNivel(int nivel) {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "selectGruposNivel", "SHAREDB");
            var parametros = DboParaParametros(new { Nivel = nivel });
            return conexao.Query<GrupoDbo>(comando, parametros).ToList();
        }

        public bool AtualizarNivelGrupo(string grupo, int nivel) {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "updateNivelGrupo", "SHAREDB");
            var parametros = DboParaParametros(new { Nome = grupo, Nivel = nivel });
            return conexao.Execute(comando, parametros) == 1;
        }

        public bool DeletarGrupo(string grupo) {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "deleteGrupo", "SHAREDB");
            var parametros = DboParaParametros(new { Nome = grupo });
            return conexao.Execute(comando, parametros) == 1;
        }
    }
}
