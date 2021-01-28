using System.Collections.Generic;
using Infraestrutura.Repositorio.Permissao.Entidade;
using Infraestrutura.Servico.Permissao.Entidade;

namespace Infraestrutura.Repositorio.Permissao.Interface
{
    public interface IPermissaoRp
    {
        bool InserirPermissao(PermissaoDpo permissao);
        List<AcessoSistemicoDpo> PesquisarAcessosUsuario(int idUsuario);
        List<AcessoSistemicoDpo> PesquisarAcessosGrupo(int idGrupo);
    }
}