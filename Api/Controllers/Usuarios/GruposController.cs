using System.Collections.Generic;
using AutoMapper;
using MandradePkgs.Retornos;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.ObjetosValor.Enum;
using Abstracoes.Representacoes.Usuario.Grupo;
using Servicos.Usuario.Interfaces;

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
        public RespostaApi<List<GrupoDto>> ListarGruposPorNivel(NivelGrupo nivel) =>
            RespostaPadrao(_grupoServico.GruposPorNivel(nivel));

        [HttpGet]
        public RespostaApi<GrupoDto> ObterDadosGrupo(string nomeGrupo) =>
            RespostaPadrao(_grupoServico.ObterDadosGrupo(nomeGrupo));


        [HttpPost]
        public RespostaApi IncluirNovoGrupo(GrupoDto grupo) =>
            RespostaPadrao(_grupoServico.InserirNovoUsuario(grupo));

        [HttpPut]
        public RespostaApi AlterarNivelGrupo(string grupo, NivelGrupo nivel, string justificativa) =>
            RespostaPadrao(_grupoServico.AtualizarNivelGrupo(grupo, nivel, justificativa));

        [HttpDelete]
        public RespostaApi ExcluirGrupo(int idGrupo) =>
            RespostaPadrao(_grupoServico.ExcluirGrupo(idGrupo));

    }
}