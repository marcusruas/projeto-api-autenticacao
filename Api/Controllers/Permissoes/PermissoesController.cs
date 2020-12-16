
using System;
using System.Collections.Generic;
using Abstracoes.Representacoes.Permissoes.Permissao;
using Aplicacao.Representacoes.Permissoes.Token;
using MandradePkgs.Retornos;
using Microsoft.AspNetCore.Mvc;
using Servicos.Permissoes.Interfaces;

namespace Api.Controllers.Usuarios
{
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
        [Route("permissoes")]
        public RespostaApi<PermissaoDto> Cadastrar(string descricao) =>
            RespostaPadrao(_servico.IncluirPermissao(descricao));

        [HttpGet]
        [Route("permissoes/{permissao}")]
        public RespostaApi<AcessoSistemicoDto> ObterAcesso(Guid permissao) =>
            RespostaPadrao(new AcessoSistemicoDto(null));
    }
}