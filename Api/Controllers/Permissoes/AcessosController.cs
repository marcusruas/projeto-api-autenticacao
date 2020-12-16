
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
    public class AcessosController : ControllerApi
    {
        private IPermissoesSrv _servico { get; }

        public AcessosController(IPermissoesSrv servico)
        {
            _servico = servico;
        }

        [HttpPost]
        [Route("acessos/autenticar")]
        public RespostaApi<TokenDto> Autenticar(
            string Usuario,
            string Senha,
            [FromServices] ConfiguracoesTokenDto configsToken,
            [FromServices] AssinaturaTokenDto assinatura
        ) =>
            RespostaPadrao(_servico.Autenticar(Usuario, Senha, configsToken, assinatura));

        [HttpGet]
        [Route("permissoes/acessos/{descricao}")]
        public RespostaApi<List<AcessoSistemicoDto>> PesquisarAcessos(string descricao) =>
            RespostaPadrao(_servico.ListarAcessos(descricao));

        [HttpGet]
        [Route("permissoes/acessos/grupos/{id}")]
        public RespostaApi<List<AcessoSistemicoDto>> ListarAcessosGrupos(int id) =>
            RespostaPadrao(_servico.ListarAcessosGrupo(id));

        [HttpGet]
        [Route("permissoes/acessos/usuarios/{id}")]
        public RespostaApi<List<AcessoSistemicoDto>> ListarAcessosUsuario(int id) =>
            RespostaPadrao(_servico.ListarAcessosUsuario(id));

        [HttpPost]
        [Route("permissoes/acessos")]
        public RespostaApi<AcessoSistemicoDto> CadastrarAcesso(InclusaoAcessoSistemicoDto Parametros) =>
            RespostaPadrao(_servico.IncluirAcesso(Parametros));

        [HttpPost]
        [Route("permissoes/acessos/grupos")]
        public RespostaApi CadastrarAcessoGrupo(int acesso, int grupo) =>
            RespostaPadrao(_servico.CadastrarAcessoGrupo(acesso, grupo));

        [HttpPost]
        [Route("permissoes/acessos/usuarios")]
        public RespostaApi CadastrarAcessoUsuario(int acesso, int idUsuario) =>
            RespostaPadrao(_servico.CadastrarAcessoUsuario(acesso, idUsuario));

        [HttpPut]
        [Route("permissoes/acessos/{id}")]
        public RespostaApi IncluirPermissaoAcesso(int id, List<Guid> permissoes) => 
            RespostaPadrao();

        [HttpPut]
        [Route("permissoes/acessos/{id}/alterar-atividade")]
        public RespostaApi AlterarAtividadeAcesso(int id) => 
            RespostaPadrao();
    }
}