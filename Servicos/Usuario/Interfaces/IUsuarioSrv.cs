using Abstracoes.Representacoes.Usuario.Grupo;
using Abstracoes.Representacoes.Usuario.Pessoa;
using Abstracoes.Representacoes.Usuario.Usuario;

namespace Servicos.Usuario.Interfaces
{
    public interface IUsuarioSrv
    {
        bool IncluirUsuario(UsuarioInclusaoDto usuario);
        PessoaDto ObterPessoaUsuario(int id);
        GrupoDto ObterGrupoUsuario(int id);
        UsuarioDto ValidarUsuario(string usuario, string senha);
    }
}