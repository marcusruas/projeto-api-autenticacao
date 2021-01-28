using MandradePkgs.Retornos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Infraestrutura.Servico.Usuario.Entidade;
using Infraestrutura.Servico.Usuario.Interface;

namespace Api.Controllers.usuarios
{

    [Route("/usuarios/grupos")]
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
        [Route("{id}")]
        public RespostaApi<GrupoDto> Get(int id) =>
            RespostaPadrao(_grupoServico.PesquisarGrupoPorId(id));

        [HttpPost]
        public RespostaApi Post(GrupoInclusaoDto grupo) =>
            RespostaPadrao(_grupoServico.InserirNovoUsuario(grupo));

        [HttpPut]
        public RespostaApi Put(int grupoPai, int grupoFilho) =>
            RespostaPadrao(_grupoServico.VincularGrupos(grupoPai, grupoFilho));

        [HttpDelete]
        [Route("{id}")]
        public RespostaApi Delete(int id) =>
            RespostaPadrao(_grupoServico.ExcluirGrupo(id));

        [HttpGet]
        public RespostaApi<List<GrupoDto>> Pesquisar([FromQuery] GrupoPesquisaDto filtro) =>
            RespostaPadrao(_grupoServico.ListarTodosGrupos(filtro));

        [HttpGet]
        [Route("{id}/pai")]
        public RespostaApi<GrupoDto> Pai(int id) =>
            RespostaPadrao(_grupoServico.ObterPai(id));

        [HttpGet]
        [Route("{id}/filhos")]
        public RespostaApi<List<GrupoDto>> Filhos(int id) =>
            RespostaPadrao(_grupoServico.ListarFilhos(id));

    }
}