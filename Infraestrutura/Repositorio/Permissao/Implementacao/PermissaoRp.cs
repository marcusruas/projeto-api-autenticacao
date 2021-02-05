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

        public List<PermissaoDpo> PesquisarPermissoes(string nome)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "selectPermissoesPorNome", "SHAREDB");
            return conexao.Query<PermissaoDpo>(comando, new { Nome = nome }).ToList();
        }
    }
}