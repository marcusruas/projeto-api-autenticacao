using Infraestrutura.Servico.Usuario.Entidade;

namespace Infraestrutura.Servicos.Usuario.Interface
{
    public interface IUsuarioSv
    {
        bool IncluirUsuario(UsuarioInclusaoDto usuario);
        bool AtualizarAtividadeUsuario(int id, bool ativo);
        bool AtualizarSenhaUsuario(int id, UsuarioAlteracaoSenhaDto alteracao);
        bool ExcluirUsuario(int id);
        PessoaDto ObterPessoaUsuario(int id);
        GrupoDto ObterGrupoUsuario(int id);
        UsuarioDto ValidarUsuario(string usuario, string senha);
        UsuarioDto PesquisarUsuario(int usuario);
    }
}