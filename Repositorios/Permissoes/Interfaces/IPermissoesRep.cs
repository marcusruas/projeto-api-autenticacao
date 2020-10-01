using System.Collections.Generic;
using Abstracoes.Representacoes.Permissoes.Permissao;

namespace Repositorios.Permissoes.Interfaces
{
    public interface IPermissoesRep
    {
        bool InserirPermissao(PermissaoDpo permissao);
        List<PermissaoDpo> PesquisarPermissoes(List<int> permissoes);
    }
}