using MandradePkgs.Retornos;
using Microsoft.AspNetCore.Mvc;
using Abstracoes.Representacoes.Usuario.Grupo;
using Servicos.Usuario.Interfaces;
using System.Collections.Generic;

namespace Api.Controllers.usuarios
{
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
        [Route("/usuarios/grupos/")]
        public RespostaApi<List<GrupoDto>> Get() =>
            RespostaPadrao(_grupoServico.ListarTodosGrupos());

        [HttpGet]
        [Route("/usuarios/grupos/{id}")]
        public RespostaApi<GrupoDto> ObterDados(int id) =>
            RespostaPadrao(_grupoServico.PesquisarGrupoPorId(id));

        [HttpGet]
        [Route("/usuarios/grupos/{id}/pai")]
        public RespostaApi<GrupoDto> ObterPai(int id) =>
            RespostaPadrao(_grupoServico.ObterPai(id));

        [HttpGet]
        [Route("/usuarios/grupos/{id}/filhos")]
        public RespostaApi<List<GrupoDto>> ObterFilhos(int id) =>
            RespostaPadrao(_grupoServico.ListarFilhos(id));

        [HttpPut]
        [Route("/usuarios/grupos")]
        public RespostaApi Put(int grupoPai, int grupoFilho) =>
            RespostaPadrao(_grupoServico.VincularGrupos(grupoPai, grupoFilho));

        [HttpPost]
        [Route("/usuarios/grupos")]
        public RespostaApi Post(GrupoInclusaoDto grupo) =>
            RespostaPadrao(_grupoServico.InserirNovoUsuario(grupo));

        [HttpDelete]
        [Route("/usuarios/grupos")]
        public RespostaApi Delete(int id) =>
            RespostaPadrao(_grupoServico.ExcluirGrupo(id));
    }
}