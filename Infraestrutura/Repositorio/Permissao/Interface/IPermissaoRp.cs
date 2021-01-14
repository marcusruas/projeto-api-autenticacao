using System.Collections.Generic;
using Infraestrutura.Repositorio.Permissao.Entidade;

namespace Infraestrutura.Repositorios.Permissao.Interface
{
    public interface IPermissaoRp
    {
        bool InserirPermissao(PermissaoDpo permissao);
    }
}