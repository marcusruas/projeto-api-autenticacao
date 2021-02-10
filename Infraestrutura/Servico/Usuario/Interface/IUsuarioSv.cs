using Infraestrutura.Servico.Usuario.Entidade;
using MandradePkgs.Autenticacao.Estrutura.Token;

namespace Infraestrutura.Servico.Usuario.Interface
{
    public interface IUsuarioSv
    {
        bool IncluirUsuario(UsuarioInclusaoDto usuario);
        bool AtualizarAtividadeUsuario(int id, bool ativo);
        bool AtualizarSenhaUsuario(int id, UsuarioAlteracaoSenhaDto alteracao);
        PessoaDto ObterPessoaUsuario(int id);
        GrupoDto ObterGrupoUsuario(int id);
        UsuarioDto ValidarUsuario(string usuario, string senha);
        UsuarioDto PesquisarUsuario(int usuario);
        Token Autenticar(string usuario, string senha, ConfiguracoesToken configsToken, AssinaturaToken assinatura);
    }
}