using System.Collections.Generic;
using infraestrutura.Servico.Permissao.Entidade;
using Infraestrutura.Servico.Permissao.Entidade;

namespace Infraestrutura.Servico.Permissao.Interface
{
    public interface IPermissaoSv
    {
        PermissaoDto IncluirPermissao(string descricao);
        List<AcessoSistemicoDto> ListarAcessos(int idUsuario);
    }
}