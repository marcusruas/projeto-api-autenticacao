using MandradePkgs.Retornos;
using Microsoft.AspNetCore.Mvc;
using Infraestrutura.Servico.Permissao.Interface;
using Infraestrutura.Servico.Permissao.Entidade;
using System.Collections.Generic;
using System;

namespace Api.Controllers.usuarios
{
    [Route("/acessos")]
    [Produces("application/json")]
    [ApiController]
    public class AcessosController : ControllerApi
    {
        private readonly IAcessoSv _servico;
        public AcessosController(IAcessoSv servico)
        {
            _servico = servico;
        }

        [HttpPost]
        public RespostaApi CadastrarAcesso(string descricao, List<int> permissoes) =>
            RespostaPadrao(_servico.IncluirAcesso(descricao, permissoes));

        [HttpPost]
        [Route("/{acesso}/grupos/{grupo}")]
        public RespostaApi CadastrarAcessoGrupo(int acesso, int grupo) =>
            RespostaPadrao(_servico.CadastrarAcessoGrupo(acesso, grupo));

        [HttpPost]
        [Route("/{acesso}/usuarios/{usuario}")]
        public RespostaApi CadastrarAcessoUsuario(int acesso, int usuario) =>
            RespostaPadrao(_servico.CadastrarAcessoUsuario(acesso, usuario));

        [HttpGet]
        [Route("/{acesso}/usuarios/{usuario}")]
        public RespostaApi<List<AcessoSistemicoDto>> ListarAcessosUsuario(int usuario) =>
            RespostaPadrao(_servico.ListarAcessosUsuario(usuario));

        [HttpGet]
        [Route("/{acesso}/grupos/{grupo}")]
        public RespostaApi<List<AcessoSistemicoDto>> ListarAcessosGrupo(int grupo) =>
            RespostaPadrao(_servico.ListarAcessosGrupo(grupo));
    }
  }