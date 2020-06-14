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
    public class GruposController : ControllerApi
    {
        private IGrupoSrv _grupoServico { get; }

        public GruposController(IGrupoSrv grupoServico)
        {
            _grupoServico = grupoServico;
        }

        [HttpGet]
        public RespostaApi<GrupoDto> ObterDadosGrupo(int Id) =>
            RespostaPadrao(_grupoServico.PesquisarGrupoPorId(Id));

        [HttpGet]
        public RespostaApi<GrupoDto> ObterPai(int Id) =>
            RespostaPadrao(_grupoServico.ObterPai(Id));

        [HttpGet]
        public RespostaApi<List<GrupoDto>> ObterFilhos(int Id) =>
            RespostaPadrao(_grupoServico.ListarFilhos(Id));

        [HttpGet]
        public RespostaApi<List<GrupoDto>> ListarTodosGrupos() =>
            RespostaPadrao(_grupoServico.ListarTodosGrupos());

        [HttpPut]
        public RespostaApi VincularGrupos(int GrupoPai, int GrupoFilho) =>
            RespostaPadrao(_grupoServico.VincularGrupos(GrupoPai, GrupoFilho));

        [HttpPost]
        public RespostaApi IncluirNovoGrupo(GrupoInclusaoDto grupo) =>
            RespostaPadrao(_grupoServico.InserirNovoUsuario(grupo));

        [HttpDelete]
        public RespostaApi ExcluirGrupo(int Id) =>
            RespostaPadrao(_grupoServico.ExcluirGrupo(Id));
    }
}