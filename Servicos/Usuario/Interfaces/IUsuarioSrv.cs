using Abstracoes.Representacoes.Usuario.Grupo;
using Abstracoes.Representacoes.Usuario.Pessoa;
using Abstracoes.Representacoes.Usuario.Usuario;

namespace Servicos.Usuario.Interfaces
{
    public interface IUsuarioSrv
    {
        bool IncluirUsuario(UsuarioInclusaoDto usuario);
        bool AtualizarAtividadeUsuario(int id, bool ativo);
        PessoaDto ObterPessoaUsuario(int id);
        GrupoDto ObterGrupoUsuario(int id);
        UsuarioDto ValidarUsuario(string usuario, string senha);
    }
}