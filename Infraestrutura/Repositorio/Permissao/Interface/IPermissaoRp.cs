using System;
using System.Collections.Generic;
using Infraestrutura.Repositorio.Permissao.Entidade;

namespace Infraestrutura.Repositorio.Permissao.Interface
{
    public interface IPermissaoRp
    {
        bool InserirPermissao(PermissaoDpo permissao);
        List<PermissaoDpo> PesquisarPermissoes(string nome);
    }
}