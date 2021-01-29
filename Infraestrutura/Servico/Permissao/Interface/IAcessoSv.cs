using System.Collections.Generic;
using Infraestrutura.Servico.Permissao.Entidade;

namespace Infraestrutura.Servico.Permissao.Interface
{
    public interface IAcessoSv
    {
        bool IncluirAcesso(string descricao, List<int> permissoes);
        List<AcessoSistemicoDto> ListarAcessosUsuario(int idUsuario);
    }
}