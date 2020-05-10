using Abstracoes.Representacoes.Usuario.Grupo;
using Abstracoes.Representacoes.Usuario.Pessoa;
using Abstracoes.Representacoes.Usuario.Usuario;

namespace Repositorios.Usuario.Interfaces
{
    public interface IUsuarioRep
    {
        bool InserirUsuario(UsuarioDpo usuario);
        bool AtualizarAtivoUsuario(int Id, bool Ativo);
        PessoaDpo BuscarPessoaUsuario(int id);
        GrupoDpo BuscarGrupoUsuario(int id);
        (UsuarioDpo, GrupoDpo, PessoaDpo) BuscarUsuario(string usuario, string senha);
    }
}