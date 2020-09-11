using Abstracoes.Representacoes.Usuario.Grupo;
using Abstracoes.Representacoes.Usuario.Pessoa;
using Abstracoes.Representacoes.Usuario.Usuario;
using Aplicacao.Representacoes.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace Servicos.Usuario.Interfaces
{
    public interface IUsuarioSrv
    {
        bool IncluirUsuario(UsuarioInclusaoDto usuario);
        bool AtualizarAtividadeUsuario(int id, bool ativo);
        bool AtualizarSenhaUsuario(UsuarioAlteracaoSenhaDto alteracao);
        bool ExcluirUsuario(int id);
        PessoaDto ObterPessoaUsuario(int id);
        GrupoDto ObterGrupoUsuario(int id);
        TokenDto Autenticar(
            string usuario,
            string senha,
            ConfiguracoesTokenDto configsToken,
            AssinaturaTokenDto assinatura
        );
    }
}