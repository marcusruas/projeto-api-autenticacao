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
        private IPessoaSrv _pessoaServico { get; }
        private IMapper _mapper { get; }

        public GruposController(IGrupoSrv grupoServico, IPessoaSrv pessoaServico, IMapper mapper)
        {
            _grupoServico = grupoServico;
            _pessoaServico = pessoaServico;
            _mapper = mapper;
        }

        [HttpGet]
        public RespostaApi<List<GrupoDto>> ListarGruposPorNivel(NivelGrupo nivel) =>
            RespostaPadrao(_mapper.Map<List<GrupoDto>>(_grupoServico.GruposPorNivel(nivel)));

        [HttpGet]
        public RespostaApi<GrupoDto> ObterDadosGrupo(string nomeGrupo) =>
            RespostaPadrao(_mapper.Map<GrupoDto>(_grupoServico.ObterDadosGrupo(nomeGrupo)));


        [HttpPost]
        public RespostaApi IncluirNovoGrupo(GrupoDto grupo) =>
            RespostaPadrao(_grupoServico.InserirNovoUsuario(grupo));

        [HttpPut]
        public RespostaApi AlterarNivelGrupo(string grupo, NivelGrupo nivel, string justificativa) =>
            RespostaPadrao(_grupoServico.AtualizarNivelGrupo(grupo, nivel, justificativa));

        [HttpDelete]
        public RespostaApi ExcluirGrupo(string grupo) =>
            RespostaPadrao(_grupoServico.ExcluirGrupo(grupo));

    }
}