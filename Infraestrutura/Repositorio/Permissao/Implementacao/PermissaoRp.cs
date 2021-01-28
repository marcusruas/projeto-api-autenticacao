using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Infraestrutura.Repositorio.Permissao.Entidade;
using Infraestrutura.Repositorio.Permissao.Interface;
using Infraestrutura.Servico.Permissao.Entidade;
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

        public bool InserirAcesso(string descricao)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "insertAcesso", "SHAREDB");
            return conexao.Execute(comando, new { Descricao = descricao }) == 1;
        }

        public bool InserirPermissao(PermissaoDpo permissao)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "insertPermissao", "SHAREDB");
            var parametros = DpoParaParametros(permissao);
            return conexao.Execute(comando, parametros) == 1;
        }

        public AcessoSistemicoDpo PesquisarAcesso(string descricao)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "selectAcessoPorDescricao", "SHAREDB");
            return conexao.Query<AcessoSistemicoDpo>(comando, new { descricao }).FirstOrDefault();
        }

        public AcessoSistemicoDpo PesquisarAcesso(int acesso)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "selectAcessoPorId", "SHAREDB");
            return conexao.Query<AcessoSistemicoDpo>(comando, new { Id = acesso }).FirstOrDefault();
        }

        public List<AcessoSistemicoDpo> PesquisarAcessosGrupo(int idGrupo)
        {
            throw new System.NotImplementedException();
        }

        public List<AcessoSistemicoDpo> PesquisarAcessosUsuario(int idUsuario)
        {
            throw new System.NotImplementedException();
        }

        public List<PermissaoDpo> PesquisarPermissoes(List<int> permissoes)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "selectPermissoesPorId", "SHAREDB");
            return conexao.Query<PermissaoDpo>(comando, new { Permissoes = permissoes }).ToList();
        }

        public bool VincularPermissaoAcesso(int idAcesso, int idPermissao)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "insertPermissaoAcesso", "SHAREDB");
            return conexao.Execute(comando, new { idAcesso, idPermissao }) == 1;
        }
    }
}