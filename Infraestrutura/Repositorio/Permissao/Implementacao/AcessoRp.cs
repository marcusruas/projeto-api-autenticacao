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
    public class AcessoRp : ClasseRepositorio, IAcessoRp
    {
        private IConexaoSQL _conexao;

        public AcessoRp(IConexaoSQL conexao) : base("Permissao")
        {
            _conexao = conexao;
        }

        public bool InserirAcesso(string descricao)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "insertAcesso", "SHAREDB");
            return conexao.Execute(comando, new { Descricao = descricao }) == 1;
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

        public bool VincularPermissaoAcesso(int idAcesso, int idPermissao)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "insertPermissaoAcesso", "SHAREDB");
            return conexao.Execute(comando, new { idAcesso, idPermissao }) == 1;
        }

        public List<AcessoSistemicoDpo> PesquisarAcessosGrupo(int idGrupo)
        {
            throw new System.NotImplementedException();
        }

        public List<AcessoSistemicoDpo> PesquisarAcessosUsuario(int idUsuario)
        {
            throw new System.NotImplementedException();
        }

        public bool CadastrarAcessoGrupo(int idAcesso, int idGrupo)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "insertAcessoGrupo", "SHAREDB");
            return conexao.Execute(comando, new { Acesso = idAcesso, Grupo = idGrupo }) == 1;
        }

        public bool CadastrarAcessoUsuario(int idAcesso, int idUsuario)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "insertAcessoUsuario", "SHAREDB");
            return conexao.Execute(comando, new { Acesso = idAcesso, Usuario = idUsuario }) == 1;
        }
    }
}