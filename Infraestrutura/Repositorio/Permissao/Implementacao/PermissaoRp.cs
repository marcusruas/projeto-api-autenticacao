using System.Collections.Generic;
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

        public bool InserirPermissao(PermissaoDpo permissao)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "insertPermissao", "SHAREDB");
            var parametros = DpoParaParametros(permissao);
            return conexao.Execute(comando, parametros) == 1;
        }

        public List<AcessoSistemicoDpo> PesquisarAcessosGrupo(int idGrupo)
        {
            throw new System.NotImplementedException();
        }

        public List<AcessoSistemicoDpo> PesquisarAcessosUsuario(int idUsuario)
        {
            throw new System.NotImplementedException();
        }
    }
}