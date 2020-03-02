using System.Collections.Generic;
using AutoMapper;
using Dominio.ObjetosValor.Enum;
using Dominio.Representacao.Usuario.Grupo;
using MandradePkgs.Retornos;
using Microsoft.AspNetCore.Mvc;
using Servico.Usuario.Interface;

namespace Api.Controllers
{
    [Route("Usuarios/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class GruposController : ControllerApi
    {
        private IGrupoSrv _servico { get; }
        private IMapper _mapper { get; }

        public GruposController(IGrupoSrv servico, IMapper mapper)
        {
            _servico = servico;
            _mapper = mapper;
        }

        [HttpGet]
        public RespostaApi<List<NivelGrupo>> ListarNiveis()
        {
            return null;
        }

        [HttpGet]
        public RespostaApi<List<GrupoDto>> ListarGruposPorNivel(int nivel) =>
            RespostaPadrao(_mapper.Map<List<GrupoDto>>(_servico.GruposPorNivel(nivel)));

        [HttpGet]
        public RespostaApi<GrupoDto> ObterDadosGrupo(string nomeGrupo) =>
            RespostaPadrao(_mapper.Map<GrupoDto>(_servico.ObterDadosGrupo(nomeGrupo)));


        [HttpPost]
        public RespostaApi IncluirNovoGrupo(GrupoDto grupo) =>
            RespostaPadrao(_servico.InserirNovoUsuario(grupo));

        [HttpPut]
        public RespostaApi AlterarNivelGrupo(string grupo, int nivel, string justificativa) =>
            RespostaPadrao(_servico.AtualizarNivelGrupo(grupo, nivel, justificativa));

        [HttpDelete]
        public RespostaApi ExcluirGrupo(string grupo) =>
            RespostaPadrao(_servico.ExcluirGrupo(grupo));
    }
}