using MandradePkgs.Retornos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Infraestrutura.Servico.Usuario.Entidade;
using Infraestrutura.Servico.Usuario.Interface;

namespace Api.Controllers.usuarios
{
    [Produces("application/json")]
    [ApiController]
    public class GruposController : ControllerApi
    {
        private IGrupoSv _grupoServico { get; }

        public GruposController(IGrupoSv grupoServico)
        {
            _grupoServico = grupoServico;
        }

        [HttpGet]
        [Route("/usuarios/grupos/{id}")]
        public RespostaApi<GrupoDto> Get(int id) =>
            RespostaPadrao(_grupoServico.PesquisarGrupoPorId(id));

        [HttpPost]
        [Route("/usuarios/grupos")]
        public RespostaApi Post(GrupoInclusaoDto grupo) =>
            RespostaPadrao(_grupoServico.InserirNovoUsuario(grupo));

        [HttpPut]
        [Route("/usuarios/grupos")]
        public RespostaApi Put(int grupoPai, int grupoFilho) =>
            RespostaPadrao(_grupoServico.VincularGrupos(grupoPai, grupoFilho));

        [HttpDelete]
        [Route("/usuarios/grupos/{id}")]
        public RespostaApi Delete(int id) =>
            RespostaPadrao(_grupoServico.ExcluirGrupo(id));

        [HttpGet]
        [Route("/usuarios/grupos/")]
        public RespostaApi<List<GrupoDto>> Pesquisar([FromQuery] GrupoPesquisaDto filtro) =>
            RespostaPadrao(_grupoServico.ListarTodosGrupos(filtro));

        [HttpGet]
        [Route("/usuarios/grupos/{id}/pai")]
        public RespostaApi<GrupoDto> Pai(int id) =>
            RespostaPadrao(_grupoServico.ObterPai(id));

        [HttpGet]
        [Route("/usuarios/grupos/{id}/filhos")]
        public RespostaApi<List<GrupoDto>> Filhos(int id) =>
            RespostaPadrao(_grupoServico.ListarFilhos(id));

    }
}