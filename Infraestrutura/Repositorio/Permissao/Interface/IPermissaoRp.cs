using System;
using System.Collections.Generic;
using Infraestrutura.Repositorio.Permissao.Entidade;
using Infraestrutura.Servico.Permissao.Entidade;

namespace Infraestrutura.Repositorio.Permissao.Interface
{
    public interface IPermissaoRp
    {
        bool InserirAcesso(string descricao);
        List<AcessoSistemicoDpo> PesquisarAcessosUsuario(int idUsuario);
        List<AcessoSistemicoDpo> PesquisarAcessosGrupo(int idGrupo);
        AcessoSistemicoDpo PesquisarAcesso(string descricao);
        AcessoSistemicoDpo PesquisarAcesso(int acesso);
        bool InserirPermissao(PermissaoDpo permissao);
        List<PermissaoDpo> PesquisarPermissoes(List<int> permissoes);
        bool VincularPermissaoAcesso(int idAcesso, int idPermissao);
    }
}