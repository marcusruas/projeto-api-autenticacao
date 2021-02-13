using MandradePkgs.Retornos;
using Microsoft.AspNetCore.Mvc;
using Infraestrutura.Servico.Permissao.Interface;
using Infraestrutura.Servico.Permissao.Entidade;
using System.Collections.Generic;
using Api.PkgAuth;

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
        [EuAutorizo("A72EF0D6-B240-412A-B2C9-21A9C214508A")]
        public RespostaApi<List<PermissaoDto>> Get(string nome) =>
            RespostaPadrao(_servico.ListarPermissoes(nome));

        [HttpGet]
        [Route("/permissoes/{permissao}")]
        public RespostaApi<PermissaoDto> ListarDadosPermissao(int permissao) =>
            RespostaPadrao(_servico.PesquisarPermissaoPorId(permissao));

        [HttpGet]
        [EuAutorizo("A72EF0D6-B240-412A-B2C9-21A9C214508A")]
        [Route("/usuarios/{usuario}/permissoes")]
        public RespostaApi<List<PermissaoDto>> ListarPermissoesUsuario(int usuario) =>
            RespostaPadrao(_servico.ListarPermissoesUsuario(usuario));

        [HttpGet]
        [Route("/usuarios/grupos/{grupo}/permissoes")]
        public RespostaApi<List<PermissaoDto>> ListarPermissoesGrupo(int grupo) =>
            RespostaPadrao(_servico.ListarPermissoesGrupo(grupo));
        
        [HttpPost]
        [Route("/usuarios/grupos/{grupo}/permissoes/{permissao}")]
        public RespostaApi CadastrarPermissoesGrupo(int permissao, int grupo) =>
            RespostaPadrao(_servico.InserirPermissoesGrupo(grupo, permissao));

        [HttpPost]
        [Route("/usuarios/{usuario}/permissoes/{permissao}")]
        public RespostaApi CadastrarPermissoesUsuario(int permissao, int usuario) =>
            RespostaPadrao(_servico.InserirPermissoesUsuario(usuario, permissao));

        [HttpPut]
        [Route("/usuarios/permissoes/{permissao}")]
        public RespostaApi AlterarAtividadePermissao(int permissao, bool ativo) =>
            RespostaPadrao(_servico.AtualizarAtividadePermissao(permissao, ativo));

        [HttpDelete]
        [Route("/usuarios/{usuario}/permissoes/{permissao}")]
        public RespostaApi ExcluirPermissaoUsuario(int permissao, int usuario) =>
            RespostaPadrao(_servico.ExcluirPermissaoUsuario(permissao, usuario));

        [HttpDelete]
        [Route("/usuarios/grupos/{grupo}/permissoes/{permissao}")]
        public RespostaApi ExcluirPermissaoGrupo(int permissao, int grupo) =>
            RespostaPadrao(_servico.ExcluirPermissaoGrupo(permissao, grupo));
    }
  }