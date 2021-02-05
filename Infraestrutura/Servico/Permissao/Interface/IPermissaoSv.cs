using System.Collections.Generic;
using Infraestrutura.Servico.Permissao.Entidade;

namespace Infraestrutura.Servico.Permissao.Interface
{
    public interface IPermissaoSv
    {
        bool IncluirPermissao(string nome, string descricao);
        List<PermissaoDto> ListarPermissoes(string nome);
    }
}