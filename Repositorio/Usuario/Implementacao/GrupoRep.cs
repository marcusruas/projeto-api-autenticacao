using Dapper;
using MandradePkgs.Conexoes;
using static MandradePkgs.Conexoes.Mapeamentos.DpoSqlMapper;
using Repositorio.Usuario.Interface;
using System.Collections.Generic;
using System.Linq;
using Abstracao.Representacao.Usuario.Grupo;

namespace Repositorio.Usuario.Implementacao
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

        public List<GrupoDpo> ObterGruposPorNivel(int nivel)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "selectGruposNivel", "SHAREDB");
            var parametros = DpoParaParametros(new { Nivel = nivel });
            return conexao.Query<GrupoDpo>(comando, parametros).ToList();
        }

        public GrupoDpo ObterDadosGrupo(string grupo)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "selectGrupoNome", "SHAREDB");
            var parametros = DpoParaParametros(new { Nome = grupo });
            return conexao.Query<GrupoDpo>(comando, parametros).FirstOrDefault();
        }

        public bool AtualizarNivelGrupo(string grupo, int nivel, string justificativa)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "updateNivelGrupo", "SHAREDB");
            var parametros = DpoParaParametros(new { Nome = grupo, Nivel = nivel, Justificativa = justificativa });
            return conexao.Execute(comando, parametros) == 1;
        }

        public bool DeletarGrupo(string grupo)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "deleteGrupo", "SHAREDB");
            var parametros = DpoParaParametros(new { Nome = grupo });
            return conexao.Execute(comando, parametros) == 1;
        }
    }
}
