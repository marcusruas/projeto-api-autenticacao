using Abstracoes.Representacoes.Usuario.Usuario;

namespace Servicos.Usuario.Interfaces
{
    public interface IUsuarioSrv
    {
        bool IncluirUsuario(UsuarioInclusaoDto usuario);
    }
}