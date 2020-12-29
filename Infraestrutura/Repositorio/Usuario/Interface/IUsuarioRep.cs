using Infraestrutura.Repositorio.Entidade;
using Infraestrutura.Repositorio.Usuario.Entidade;

namespace Repositorios.Usuario.Interfaces
{
    public interface IUsuarioRep
    {
        bool InserirUsuario(UsuarioDpo usuario);
        bool AtualizarAtivoUsuario(int id, bool ativo);
        bool AtualizarSenhaUsuario(int id, string senhaAntiga, string senhaNova);
        bool DeletarUsuario(int id);
        PessoaDpo BuscarPessoaUsuario(int id);
        GrupoDpo BuscarGrupoUsuario(int id);
        (UsuarioDpo, GrupoDpo, PessoaDpo) BuscarUsuario(string usuario, string senha);
        (UsuarioDpo, GrupoDpo, PessoaDpo) BuscarUsuario(int usuario);
    }
}