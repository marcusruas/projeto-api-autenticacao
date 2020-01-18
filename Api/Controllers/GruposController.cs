using System;
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
    [ApiController]
    public class GruposController : ControllerApi {
        private IGrupoSrv _servico { get; }
        private IMapper _mapper { get; }

        public GruposController(IGrupoSrv servico, IMapper mapper) {
            _servico = servico;
            _mapper = mapper;
        }

        [HttpPost]
        public RespostaApi IncluirNovoGrupo(string nome, string descricao, NivelGrupo nivel) =>
            RespostaPadrao(_servico.InserirNovoUsuario(nome, descricao, nivel));

        [HttpGet]
        public RespostaApi<List<NivelGrupoDto>> ListarNiveisGrupo() {
            var niveis = _mapper.Map<List<NivelGrupoDto>>(Enum.GetValues(typeof(NivelGrupo)).Cast<NivelGrupo>());
            return RespostaPadrao(niveis);
        }

        [HttpGet]
        public RespostaApi<List<GrupoDto>> ListarGruposPorNivel(int nivel) =>
            RespostaPadrao(_mapper.Map<List<GrupoDto>>(_servico.GruposPorNivel(nivel)));
    }
}