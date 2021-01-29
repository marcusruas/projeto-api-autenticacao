using System.Collections.Generic;
using Infraestrutura.Servico.Permissao.Entidade;

namespace Infraestrutura.Servico.Permissao.Interface
{
    public interface IAcessoSv
    {
        bool IncluirAcesso(string descricao, List<int> permissoes);
        List<AcessoSistemicoDto> ListarAcessosUsuario(int idUsuario);
        List<AcessoSistemicoDto> ListarAcessosGrupo(int idGrupo);
        bool CadastrarAcessoGrupo(int idAcesso, int idGrupo);
        bool CadastrarAcessoUsuario(int idAcesso, int idUsuario);
    }
}