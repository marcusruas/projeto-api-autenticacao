using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Infraestrutura.Repositorio.Permissao.Entidade;
using Infraestrutura.Repositorio.Permissao.Interface;
using MandradePkgs.Conexoes;
using MandradePkgs.Conexoes.Estrutura.Model;
using static MandradePkgs.Conexoes.Estrutura.Mapeamento.DpoSqlMapper;

namespace Infraestrutura.Repositorio.Permissao.Implementacao
{
    public class PermissaoRp : ClasseRepositorio, IPermissaoRp
    {
        private IConexaoSQL _conexao;

        public PermissaoRp(IConexaoSQL conexao) : base("Permissao")
        {
            _conexao = conexao;
        }

        public bool InserirPermissao(PermissaoDpo permissao)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "insertPermissao", "SHAREDB");
            var parametros = DpoParaParametros(permissao);
            return conexao.Execute(comando, parametros) == 1;
        }

        public PermissaoDpo PesquisarPermissaoPorId(int permissao)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "selectPermissoesPorId", "SHAREDB");
            return conexao.QueryFirstOrDefault<PermissaoDpo>(comando, new { permissao });
        }

        public List<PermissaoDpo> PesquisarPermissoes(string nome)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "selectPermissoesPorNome", "SHAREDB");
            return conexao.Query<PermissaoDpo>(comando, new { nome }).ToList();
        }

        public List<PermissaoDpo> PesquisarPermissoesUsuario(int usuario)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "selectPermissoesUsuario", "SHAREDB");
            return conexao.Query<PermissaoDpo>(comando, new { usuario }).ToList();
        }

        public List<PermissaoDpo> PesquisarPermissoesGrupo(int grupo)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "selectPermissoesGrupo", "SHAREDB");
            return conexao.Query<PermissaoDpo>(comando, new { grupo }).ToList();
        }

        public bool InserirPermissaoUsuario(int usuario, int permissao)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "insertPermissaoUsuario", "SHAREDB");
            return conexao.Execute(comando, new { permissao, usuario }) == 1;
        }

        public bool InserirPermissaoGrupo(int grupo, int permissao)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "insertPermissaoGrupo", "SHAREDB");
            return conexao.Execute(comando, new { permissao, grupo }) == 1;
        }
    }
}