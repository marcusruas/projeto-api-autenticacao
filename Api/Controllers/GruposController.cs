﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MandradePkgs.Retornos;
using Microsoft.AspNetCore.Mvc;
using Servico.Grupo.Interface;
using Aplicacao.Grupo;
using Dominio.Grupo;

namespace Api.Controllers
{
    [Route("[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class GruposController : ControllerApi {
        private IGrupoSrv _servico { get; }
        private IMapper _mapper { get; }

        public GruposController(IGrupoSrv servico, IMapper mapper) {
            _servico = servico;
            _mapper = mapper;
        }

        [HttpGet]
        public RespostaApi<List<NivelGrupoDto>> ListarNiveis() {
            var niveis = _mapper.Map<List<NivelGrupoDto>>(Enum.GetValues(typeof(NivelGrupo)).Cast<NivelGrupo>());
            return RespostaPadrao(niveis);
        }

        [HttpGet]
        public RespostaApi<List<GrupoDto>> ListarGruposPorNivel(int nivel) =>
            RespostaPadrao(_mapper.Map<List<GrupoDto>>(_servico.GruposPorNivel(nivel)));

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