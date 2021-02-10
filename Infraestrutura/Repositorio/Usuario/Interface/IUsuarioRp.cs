using Infraestrutura.Repositorio.Entidade;
using Infraestrutura.Repositorio.Usuario.Entidade;

namespace Infraestrutura.Repositorio.Usuario.Interface
{
    public interface IUsuarioRp
    {
        bool InserirUsuario(UsuarioDpo usuario);
        bool AtualizarAtivoUsuario(int id, bool ativo);
        bool AtualizarSenhaUsuario(int id, string senhaAntiga, string senhaNova);
        PessoaDpo BuscarPessoaUsuario(int id);
        GrupoDpo BuscarGrupoUsuario(int id);
        (UsuarioDpo, GrupoDpo, PessoaDpo) BuscarUsuario(string usuario, string senha);
        (UsuarioDpo, GrupoDpo, PessoaDpo) BuscarUsuario(int usuario);
        //Token Autenticar(string usuario, string senha, ConfiguracoesToken configsToken, AssinaturaToken assinatura);
    }
}