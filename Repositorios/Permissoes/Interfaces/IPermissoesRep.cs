using System.Collections.Generic;
using Abstracoes.Representacoes.Permissoes.Permissao;

namespace Repositorios.Permissoes.Interfaces
{
    public interface IPermissoesRep
    {
        bool InserirAcesso(string acesso);
        bool InserirPermissao(PermissaoDpo permissao);
        bool VincularPermissaoAcesso(int idAcesso, int idPermissao);
        List<PermissaoDpo> PesquisarPermissoes(List<int> permissoes);
        AcessoSistemicoDpo PesquisarAcesso(string descricao);
        List<AcessoSistemicoDpo> PesquisarAcessos(string descricao);
    }
}