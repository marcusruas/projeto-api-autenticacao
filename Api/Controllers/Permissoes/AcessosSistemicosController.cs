using MandradePkgs.Retornos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Infraestrutura.Servico.Usuario.Entidade;

namespace Api.Controllers.usuarios
{
    [Produces("application/json")]
    [Route("Acessos")]
    [ApiController]
    public class AcessosSistemicosController : ControllerApi
    {

        public AcessosSistemicosController()
        {
        }

        // [HttpPost]
        // [Route("permissoes")]
        // public RespostaApi<PermissaoDto> Cadastrar(string descricao) =>
        //     RespostaPadrao(_servico.IncluirPermissao(descricao));

        // [HttpGet]
        // [Route("permissoes/{permissao}")]
        // public RespostaApi<AcessoSistemicoDto> ObterAcesso(Guid permissao) =>
        //     RespostaPadrao(new AcessoSistemicoDto(null));
    }
}