using Dapper;
using MandradePkgs.Conexoes;
using static MandradePkgs.Conexoes.Mapeamentos.DpoSqlMapper;
using Repositorios.Usuario.Interfaces;
using System.Collections.Generic;
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

        public List<GrupoDpo> ObterGruposPorNivel(int nivel)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "selectGruposNivel", "SHAREDB");
            var parametros = DpoParaParametros(new { Nivel = nivel });
            return conexao.Query<GrupoDpo>(comando, parametros).ToList();
        }

        public bool AtualizarNivelGrupo(GrupoAtualizacaoDto atualizacao)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "updateNivelGrupo", "SHAREDB");
            var parametros = DpoParaParametros(new
            {
                Id = atualizacao.Id,
                Nivel = atualizacao.Nivel,
                Justificativa = atualizacao.Justificativa
            });
            return conexao.Execute(comando, parametros) == 1;
        }

        public bool DeletarGrupo(int id)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "deleteGrupo", "SHAREDB");
            var parametros = DpoParaParametros(new { id });
            return conexao.Execute(comando, parametros) == 1;
        }

        public GrupoDpo ObterGrupoPorId(int id)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "selectGrupoPorId", "SHAREDB");
            var parametros = DpoParaParametros(new { Id = id });
            return conexao.Query<GrupoDpo>(comando, parametros).FirstOrDefault();
        }
    }
}
