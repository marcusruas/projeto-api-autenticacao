using Abstracoes.Representacoes.Usuario.Usuario;

namespace Repositorios.Usuario.Interfaces
{
    public interface IUsuarioRep
    {
        bool InserirUsuario(UsuarioDpo usuario);
    }
}