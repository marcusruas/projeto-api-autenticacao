using MandradePkgs.Retornos;
using Microsoft.AspNetCore.Mvc;
using Infraestrutura.Servico.Permissao.Interface;
using Infraestrutura.Servico.Permissao.Entidade;

namespace Api.Controllers.usuarios
{
    [Route("/permissoes")]
    [Produces("application/json")]
    [ApiController]
    public class PermissoesController : ControllerApi
    {
        private readonly IPermissaoSv _servico;
        public PermissoesController(IPermissaoSv servico)
        {
            _servico = servico;
        }

        [HttpPost]
        [Route("/permissoes")]
        public RespostaApi Cadastrar(string descricao) =>
            RespostaPadrao(_servico.IncluirPermissao(descricao));
    }
  }