using Dapper;
using MandradePkgs.Conexoes;
using static MandradePkgs.Conexoes.Mapeamentos.DpoSqlMapper;
using Repositorios.Usuario.Interfaces;
using System.Linq;
using Abstracoes.Representacoes.Usuario.Grupo;
using System.Collections.Generic;

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

        public bool VincularGrupos(int grupoPai, int grupoFilho)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "updateGrupoVinculo", "SHAREDB");
            var parametros = DpoParaParametros(new { grupoPai, grupoFilho });
            return conexao.Execute(comando, parametros) == 1;
        }

        public GrupoDpo ObterPai(int id)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "selectGrupoPai", "SHAREDB");
            var parametros = DpoParaParametros(new { Id = id });
            return conexao.Query<GrupoDpo>(comando, parametros).FirstOrDefault();
        }

        public List<GrupoDpo> ObterFilhos(int id)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "selectGrupoFilhos", "SHAREDB");
            var parametros = DpoParaParametros(new { Id = id });
            return conexao.Query<GrupoDpo>(comando, parametros).ToList();
        }

        public List<GrupoDpo> ObterGrupos(GrupoPesquisaDto filtro)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "selectGrupos", "SHAREDB");

            var builder = new SqlBuilder();
            var selector = builder.AddTemplate(comando);

            if (!string.IsNullOrWhiteSpace(filtro.Nome))
                builder.Where("NOME LIKE @NOME", new { Nome = $"%{filtro.Nome}%" });

            if (!string.IsNullOrWhiteSpace(filtro.Descricao))
                builder.Where("DESCRICAO LIKE @DESCRICAO", new { Descricao = $"%{filtro.Descricao}%" });

            return conexao.Query<GrupoDpo>(selector.RawSql, selector.Parameters).ToList();
        }
    }
}
