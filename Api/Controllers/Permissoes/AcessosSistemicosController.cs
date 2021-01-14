using MandradePkgs.Retornos;
using Microsoft.AspNetCore.Mvc;
using Infraestrutura.Servico.Permissao.Interface;
using infraestrutura.Servico.Permissao.Entidade;

namespace Api.Controllers.usuarios
{
    [Route("/AcessosSistemicos/")]
    [Produces("application/json")]
    [ApiController]
    public class AcessosSistemicosController : ControllerApi
    {
        private readonly IPermissaoSv _servico;
        public AcessosSistemicosController(IPermissaoSv servico)
        {
            _servico = servico;
        }

        [HttpPost]
        [Route("/permissoes/")]
        public RespostaApi<PermissaoDto> Cadastrar(string descricao) =>
            RespostaPadrao(_servico.IncluirPermissao(descricao));

        // [HttpGet]
        // [Route("permissoes/{permissao}")]
        // public RespostaApi<AcessoSistemicoDto> ObterAcesso(Guid permissao) =>
        //     RespostaPadrao(new AcessoSistemicoDto(null));
    }
}