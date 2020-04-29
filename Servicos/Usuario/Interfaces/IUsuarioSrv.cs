using Abstracoes.Representacoes.Usuario.Usuario;

namespace Servicos.Usuario.Interfaces
{
    public interface IUsuarioSrv
    {
        void IncluirUsuario(UsuarioInclusaoDto usuario);
    }
}