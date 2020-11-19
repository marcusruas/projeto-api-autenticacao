using System;
using System.Collections.Generic;
using Abstracoes.Representacoes.Permissoes.Permissao;
using Aplicacao.Representacoes.Permissoes.Token;

namespace Servicos.Permissoes.Interfaces
{
    public interface IPermissoesSrv
    {
        TokenDto Autenticar(string usuario, string senha, ConfiguracoesTokenDto configsToken, AssinaturaTokenDto assinatura);
        PermissaoDto IncluirPermissao(string descricao);
        AcessoSistemicoDto IncluirAcesso(InclusaoAcessoSistemicoDto parametros);
        List<AcessoSistemicoDto> ListarAcessos(string descricao);
        bool CadastrarAcessoGrupo(int acesso, int grupo);
        bool CadastrarAcessoUsuario(int acesso, int usuario);
    }
}