using MandradePkgs.Retornos;
using Microsoft.AspNetCore.Mvc;
using Abstracoes.Representacoes.Usuario.Grupo;
using Servicos.Usuario.Interfaces;
using System.Collections.Generic;

namespace Api.Controllers.Usuarios
{
    [Route("Usuarios/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class GrupoController : ControllerApi
    {
        private IGrupoSrv _grupoServico { get; }

        public GrupoController(IGrupoSrv grupoServico)
        {
            _grupoServico = grupoServico;
        }

        [HttpGet]
        public RespostaApi<GrupoDto> ObterDados(int Id) =>
            RespostaPadrao(_grupoServico.PesquisarGrupoPorId(Id));

        [HttpGet]
        public RespostaApi<GrupoDto> ObterPai(int Id) =>
            RespostaPadrao(_grupoServico.ObterPai(Id));

        [HttpGet]
        public RespostaApi<List<GrupoDto>> ObterFilhos(int Id) =>
            RespostaPadrao(_grupoServico.ListarFilhos(Id));

        [HttpGet]
        public RespostaApi<List<GrupoDto>> ListarTodos() =>
            RespostaPadrao(_grupoServico.ListarTodosGrupos());

        [HttpPut]
        public RespostaApi Vincular(int GrupoPai, int GrupoFilho) =>
            RespostaPadrao(_grupoServico.VincularGrupos(GrupoPai, GrupoFilho));

        [HttpPost]
        public RespostaApi Cadastrar(GrupoInclusaoDto grupo) =>
            RespostaPadrao(_grupoServico.InserirNovoUsuario(grupo));

        [HttpDelete]
        public RespostaApi Excluir(int Id) =>
            RespostaPadrao(_grupoServico.ExcluirGrupo(Id));
    }
}