using System;
using Abstracoes.Representacoes.Permissoes.Permissao;
using Aplicacao.Representacoes.Permissoes.Token;

namespace Servicos.Permissoes.Interfaces
{
    public interface IPermissoesSrv
    {
        TokenDto Autenticar(string usuario, string senha, ConfiguracoesTokenDto configsToken, AssinaturaTokenDto assinatura);
        PermissaoDto IncluirPermissao(string descricao);
    }
}