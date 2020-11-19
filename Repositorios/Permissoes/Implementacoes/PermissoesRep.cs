using System.Collections.Generic;
using System.Linq;
using Abstracoes.Representacoes.Permissoes.Permissao;
using Dapper;
using MandradePkgs.Conexoes;
using Repositorios.Permissoes.Interfaces;
using static MandradePkgs.Conexoes.Mapeamentos.DpoSqlMapper;

namespace Repositorios.Permissoes.Implementacoes
{
    public class PermissoesRep : IPermissoesRep
    {
        private IConexaoSQL _conexao;

        public PermissoesRep(IConexaoSQL conexao)
        {
            _conexao = conexao;
        }

        public bool InserirPermissao(PermissaoDpo permissao)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "insertPermissao", "SHAREDB");
            var parametros = DpoParaParametros(permissao);
            return conexao.Execute(comando, parametros) == 1;
        }

        public bool InserirAcesso(string acesso)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "insertAcesso", "SHAREDB");
            return conexao.Execute(comando, new { Descricao = acesso }) == 1;
        }

        public bool VincularPermissaoAcesso(int idAcesso, int idPermissao)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "insertPermissaoAcesso", "SHAREDB");
            return conexao.Execute(comando, new { idAcesso, idPermissao }) == 1;
        }

        public List<PermissaoDpo> PesquisarPermissoes(List<int> permissoes)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "selectPermissoesPorId", "SHAREDB");
            return conexao.Query<PermissaoDpo>(comando, new { Permissoes = permissoes }).ToList();
        }

        public AcessoSistemicoDpo PesquisarAcesso(string descricao)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "selectAcessoPorDescricao", "SHAREDB");
            return conexao.Query<AcessoSistemicoDpo>(comando, new { descricao }).FirstOrDefault();
        }

        public List<AcessoSistemicoDpo> PesquisarAcessos(string descricao)
        {
            List<AcessoSistemicoDpo> retorno = new List<AcessoSistemicoDpo>();
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "selectAcessosPermissoes", "SHAREDB");
            using (var grid = conexao.QueryMultiple(comando, new { descricao })) {
                retorno = grid.Read<AcessoSistemicoDpo>().ToList();

                if(!retorno.Any())
                    return new List<AcessoSistemicoDpo>();
                
                List<PermissaoAcessoDpo> permissoes = grid.Read<PermissaoAcessoDpo>().ToList();

                foreach(var acesso in retorno) {
                    List<PermissaoDpo> permissoesAcesso = permissoes
                        .Where(p => p.Acesso == acesso.Id)
                        .Select(p => new PermissaoDpo(p)).ToList();

                    acesso.Permissoes.AddRange(permissoesAcesso);
                }
            }

            return retorno;
        }

        public AcessoSistemicoDpo PesquisarAcesso(int acesso)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "selectAcessoPorId", "SHAREDB");
            return conexao.Query<AcessoSistemicoDpo>(comando, new { Id = acesso }).FirstOrDefault();
        }

        public bool InserirAcessoGrupo(int acesso, int grupo)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "insertAcessoGrupo", "SHAREDB");
            return conexao.Execute(comando, new { Acesso = acesso, Grupo = grupo }) == 1;
        }

        public bool InserirAcessoUsuario(int acesso, int usuario)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "insertAcessoUsuario", "SHAREDB");
            return conexao.Execute(comando, new { Acesso = acesso, Usuario = usuario }) == 1;
        }
    }
}