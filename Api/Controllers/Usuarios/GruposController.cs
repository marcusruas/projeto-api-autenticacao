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
        public RespostaApi<GrupoDto> ObterDadosGrupo(int id) =>
            RespostaPadrao(_grupoServico.PesquisarGrupoPorId(id));


        [HttpPost]
        public RespostaApi IncluirNovoGrupo(GrupoDto grupo) =>
            RespostaPadrao(_grupoServico.InserirNovoUsuario(grupo));

        [HttpPut]
        public RespostaApi AlterarNivelGrupo(GrupoAtualizacaoDto atualizacao) =>
            RespostaPadrao(_grupoServico.AtualizarNivelGrupo(atualizacao));

        [HttpDelete]
        public RespostaApi ExcluirGrupo(int id) =>
            RespostaPadrao(_grupoServico.ExcluirGrupo(id));

    }
}