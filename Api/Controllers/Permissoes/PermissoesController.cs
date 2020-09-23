
using Abstracoes.Representacoes.Permissoes.Permissao;
using Aplicacao.Representacoes.Permissoes.Token;
using MandradePkgs.Retornos;
using Microsoft.AspNetCore.Mvc;
using Servicos.Permissoes.Interfaces;

namespace Api.Controllers.Usuarios
{
    [Route("[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class PermissoesController : ControllerApi
    {
        private IPermissoesSrv _servico { get; }

        public PermissoesController(IPermissoesSrv servico)
        {
            _servico = servico;
        }

        [HttpPost]
        public RespostaApi<TokenDto> Autenticar(
            string Usuario,
            string Senha,
            [FromServices] ConfiguracoesTokenDto configsToken,
            [FromServices] AssinaturaTokenDto assinatura
        ) =>
            RespostaPadrao(_servico.Autenticar(Usuario, Senha, configsToken, assinatura));

        [HttpPost]
        public RespostaApi<PermissaoDto> Cadastrar(string Descricao) =>
            RespostaPadrao(_servico.IncluirPermissao(Descricao));
    }
}