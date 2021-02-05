using MandradePkgs.Retornos;
using Microsoft.AspNetCore.Mvc;
using Infraestrutura.Servico.Permissao.Interface;
using Infraestrutura.Servico.Permissao.Entidade;
using System.Collections.Generic;
using System;

namespace Api.Controllers.usuarios
{
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
        [Route("/usuarios/permissoes")]
        public RespostaApi Cadastrar(string nome, string descricao) =>
            RespostaPadrao(_servico.IncluirPermissao(nome, descricao));

        [HttpGet]
        [Route("/usuarios/permissoes")]
        public RespostaApi<List<PermissaoDto>> Get(string nome) =>
            RespostaPadrao(_servico.ListarPermissoes(nome));

        [HttpGet]
        [Route("/usuarios/{usuario}/permissoes")]
        public RespostaApi<List<PermissaoDto>> ListarPermissoesUsuario(int usuario) =>
            throw new NotImplementedException();

        [HttpGet]
        [Route("/usuarios/grupos/{grupo}/permissoes")]
        public RespostaApi<List<PermissaoDto>> ListarPermissoesGrupo(int grupo) =>
            throw new NotImplementedException();
        
        [HttpPost]
        [Route("/usuarios/grupos/{grupo}/permissoes/{permissao}")]
        public RespostaApi CadastrarPermissoesGrupo(int permissao, int grupo) =>
            throw new NotImplementedException();

        [HttpPost]
        [Route("/usuarios/{usuario}/permissoes/{permissao}")]
        public RespostaApi CadastrarPermissoesUsuario(int permissao, int usuario) =>
            throw new NotImplementedException();

        [HttpPut]
        [Route("/usuarios/permissoes/{permissao}")]
        public RespostaApi AlterarAtividadePermissao(int permissao, bool ativo) =>
            throw new NotImplementedException();

        [HttpGet]
        [Route("/permissoes/{permissao}")]

        public RespostaApi ListarDadosPermissao(int permissao) =>
            throw new NotImplementedException();
    }
  }