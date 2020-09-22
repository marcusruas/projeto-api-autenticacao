using Aplicacao.Representacoes.Usuario;

namespace Servicos.Permissoes.Interfaces
{
    public interface IPermissoesSrv
    {
        TokenDto Autenticar(string usuario, string senha, ConfiguracoesTokenDto configsToken, AssinaturaTokenDto assinatura);
    }
}