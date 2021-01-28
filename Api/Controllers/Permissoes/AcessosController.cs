using MandradePkgs.Retornos;
using Microsoft.AspNetCore.Mvc;
using Infraestrutura.Servico.Permissao.Interface;
using infraestrutura.Servico.Permissao.Entidade;
using Infraestrutura.Servico.Permissao.Entidade;
using System.Collections.Generic;
using Servico.Permissao.Entidade;
using System;

namespace Api.Controllers.usuarios
{
    [Route("/acessos")]
    [Produces("application/json")]
    [ApiController]
    public class AcessosController : ControllerApi
    {
        private readonly IPermissaoSv _servico;
        public AcessosController(IPermissaoSv servico)
        {
            _servico = servico;
        }

        [HttpPost]
        public RespostaApi<PermissaoDto> CadastrarAcesso(AcessoInclusaoDto acesso) =>
            throw new NotImplementedException();

        [HttpPost]
        [Route("/{acesso}/grupos/{grupo}")]
        public RespostaApi<PermissaoDto> CadastrarAcessoGrupo(int acesso, int grupo) =>
            throw new NotImplementedException();

        [HttpPost]
        [Route("/{acesso}/usuarios/{usuario}")]
        public RespostaApi<PermissaoDto> CadastrarAcessoUsuario(int acesso, int usuario) =>
            throw new NotImplementedException();

        [HttpGet]
        [Route("/{acesso}/usuarios/{usuario}")]
        public RespostaApi<List<AcessoSistemicoDto>> ListarAcessosUsuario(int usuario) =>
            RespostaPadrao(_servico.ListarAcessos(usuario));

        [HttpGet]
        [Route("/{acesso}/grupos/{grupo}")]
        public RespostaApi<List<AcessoSistemicoDto>> ListarAcessosGrupo(int grupo) =>
            RespostaPadrao(_servico.ListarAcessos(grupo));

        [HttpPost]
        [Route("/permissoes")]
        public RespostaApi<PermissaoDto> Cadastrar(string descricao) =>
            RespostaPadrao(_servico.IncluirPermissao(descricao));
    }
  }