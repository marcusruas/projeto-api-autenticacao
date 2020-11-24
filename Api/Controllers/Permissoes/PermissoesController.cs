
using System.Collections.Generic;
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

        [HttpGet]
        public RespostaApi<List<AcessoSistemicoDto>> PesquisarAcessos(string descricao) =>
            RespostaPadrao(_servico.ListarAcessos(descricao));

        [HttpGet]
        public RespostaApi<List<AcessoSistemicoDto>> ListarAcessosGrupos(int idGrupo) =>
            RespostaPadrao(_servico.ListarAcessosGrupo(idGrupo));

        [HttpGet]
        public RespostaApi<List<AcessoSistemicoDto>> ListarAcessosUsuario(int idUsuario) =>
            RespostaPadrao(_servico.ListarAcessosUsuario(idUsuario));

        [HttpPost]
        public RespostaApi<PermissaoDto> Cadastrar(string Descricao) =>
            RespostaPadrao(_servico.IncluirPermissao(Descricao));

        [HttpPost]
        public RespostaApi<AcessoSistemicoDto> CadastrarAcesso(InclusaoAcessoSistemicoDto Parametros) =>
            RespostaPadrao(_servico.IncluirAcesso(Parametros));

        [HttpPost]
        public RespostaApi CadastrarAcessoGrupo(int idAcesso, int idGrupo) =>
            RespostaPadrao(_servico.CadastrarAcessoGrupo(idAcesso, idGrupo));

        [HttpPost]
        public RespostaApi CadastrarAcessoUsuario(int idAcesso, int idUsuario) =>
            RespostaPadrao(_servico.CadastrarAcessoUsuario(idAcesso, idUsuario));

        [HttpPut]
        public RespostaApi IncluirPermissaoAcesso(int idAcesso, int idPermissao) => 
            RespostaPadrao();

        [HttpPut]
        public RespostaApi AlterarAcessoGrupo(int idAcesso) => 
            RespostaPadrao();

        [HttpPut]
        public RespostaApi AlterarAcessoUsuario(int idAcesso) => 
            RespostaPadrao();
    }
}