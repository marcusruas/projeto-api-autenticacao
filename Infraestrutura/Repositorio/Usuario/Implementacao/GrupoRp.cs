using Dapper;
using MandradePkgs.Conexoes;
using static MandradePkgs.Conexoes.Estrutura.Mapeamento.DpoSqlMapper;
using Infraestrutura.Repositorio.Usuario.Interface;
using System.Linq;
using Infraestrutura.Repositorio.Usuario.Entidade;
using System.Collections.Generic;
using System.IO;
using System;
using MandradePkgs.Conexoes.Estrutura.Model;

namespace Infraestrutura.Repositorio.Usuario.Implementacao
{
    public class GrupoRp : ClasseRepositorio, IGrupoRp
    {
        private IConexaoSQL _conexao { get; set; }

        public GrupoRp(IConexaoSQL conexao) : base("Usuario")
        {
            _conexao = conexao;
        }

        public bool AdicionarGrupo(GrupoDpo grupo)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "insertGrupo", "SHAREDB");
            var parametros = DpoParaParametros(grupo);
            return conexao.Execute(comando, parametros) == 1;
        }

        public GrupoDpo ObterGrupoPorId(int id)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "selectGrupoPorId", "SHAREDB");
            var parametros = DpoParaParametros(new { Id = id });
            return conexao.Query<GrupoDpo>(comando, parametros).FirstOrDefault();
        }

        public bool DeletarGrupo(int id)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "deleteGrupo", "SHAREDB");
            var parametros = DpoParaParametros(new { id });
            return conexao.Execute(comando, parametros) == 1;
        }

        public bool VincularGrupos(int grupoPai, int grupoFilho)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "updateGrupoVinculo", "SHAREDB");
            var parametros = DpoParaParametros(new { grupoPai, grupoFilho });
            return conexao.Execute(comando, parametros) == 1;
        }

        public GrupoDpo ObterPai(int id)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "selectGrupoPai", "SHAREDB");
            var parametros = DpoParaParametros(new { Id = id });
            return conexao.Query<GrupoDpo>(comando, parametros).FirstOrDefault();
        }

        public List<GrupoDpo> ObterFilhos(int id)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "selectGrupoFilhos", "SHAREDB");
            var parametros = DpoParaParametros(new { Id = id });
            return conexao.Query<GrupoDpo>(comando, parametros).ToList();
        }

        public List<GrupoDpo> ObterGrupos(string nome, string descricao)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "selectGrupos", "SHAREDB");

            var builder = new SqlBuilder();
            var selector = builder.AddTemplate(comando);

            if (!string.IsNullOrWhiteSpace(nome))
                builder.Where("NOME LIKE @NOME", new { Nome = $"%{nome}%" });

            if (!string.IsNullOrWhiteSpace(descricao))
                builder.Where("DESCRICAO LIKE @DESCRICAO", new { Descricao = $"%{descricao}%" });

            return conexao.Query<GrupoDpo>(selector.RawSql, selector.Parameters).ToList();
        }
    }
}
