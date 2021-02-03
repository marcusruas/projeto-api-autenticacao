using System.Collections.Generic;
using System.Linq;
using Dapper;
using Infraestrutura.Repositorio.Permissao.Entidade;
using Infraestrutura.Repositorio.Permissao.Interface;
using Infraestrutura.Servico.Permissao.Entidade;
using MandradePkgs.Conexoes;
using MandradePkgs.Conexoes.Estrutura.Model;
using Slapper;

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
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "selectAcessoSGrupo", "SHAREDB");
            var lista = conexao.Query<dynamic>(comando, new { grupo = idGrupo });

            AutoMapper.Configuration.AddIdentifier(typeof(AcessoSistemicoDpo), "ID");
            AutoMapper.Configuration.AddIdentifier(typeof(PermissaoDpo), "ID");

            var retorno = (AutoMapper.MapDynamic<AcessoSistemicoDpo>(lista) as IEnumerable<AcessoSistemicoDpo>).ToList();

            return retorno;
        }

        public List<AcessoSistemicoDpo> PesquisarAcessosUsuario(int idUsuario)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "selectAcessosUsuario", "SHAREDB");
            var lista = conexao.Query<dynamic>(comando, new { usuario = idUsuario });

            AutoMapper.Configuration.AddIdentifier(typeof(AcessoSistemicoDpo), "ID");
            AutoMapper.Configuration.AddIdentifier(typeof(PermissaoDpo), "ID");

            var retorno = (AutoMapper.MapDynamic<AcessoSistemicoDpo>(lista) as IEnumerable<AcessoSistemicoDpo>).ToList();

            return retorno;
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