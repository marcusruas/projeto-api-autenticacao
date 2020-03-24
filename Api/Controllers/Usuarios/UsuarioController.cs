using System.Collections.Generic;
using AutoMapper;
using Dominio.ObjetosValor.Enum;
using Dominio.Representacao.Usuario.Grupo;
using Dominio.Representacao.Usuario.Pessoa;
using MandradePkgs.Retornos;
using Microsoft.AspNetCore.Mvc;
using Servico.Usuario.Interface;

namespace Api.Controllers.Usuarios
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UsuariosController : ControllerApi
    {
        private IGrupoSrv _grupoServico { get; }
        private IPessoaSrv _pessoaServico { get; }
        private IMapper _mapper { get; }

        public UsuariosController(IGrupoSrv grupoServico, IPessoaSrv pessoaServico, IMapper mapper)
        {
            _grupoServico = grupoServico;
            _pessoaServico = pessoaServico;
            _mapper = mapper;
        }

        #region Grupos
        [HttpGet]
        [Route("Grupos/[action]")]
        public RespostaApi<List<GrupoDto>> ListarGruposPorNivel(NivelGrupo nivel) =>
            RespostaPadrao(_mapper.Map<List<GrupoDto>>(_grupoServico.GruposPorNivel(nivel)));

        [HttpGet]
        [Route("Grupos/[action]")]
        public RespostaApi<GrupoDto> ObterDadosGrupo(string nomeGrupo) =>
            RespostaPadrao(_mapper.Map<GrupoDto>(_grupoServico.ObterDadosGrupo(nomeGrupo)));


        [HttpPost]
        [Route("Grupos/[action]")]
        public RespostaApi IncluirNovoGrupo(GrupoDto grupo) =>
            RespostaPadrao(_grupoServico.InserirNovoUsuario(grupo));

        [HttpPut]
        [Route("Grupos/[action]")]
        public RespostaApi AlterarNivelGrupo(string grupo, NivelGrupo nivel, string justificativa) =>
            RespostaPadrao(_grupoServico.AtualizarNivelGrupo(grupo, nivel, justificativa));

        [HttpDelete]
        [Route("Grupos/[action]")]
        public RespostaApi ExcluirGrupo(string grupo) =>
            RespostaPadrao(_grupoServico.ExcluirGrupo(grupo));
        #endregion

        #region Pessoas
        [HttpPost]
        [Route("Pessoas/[action]")]
        public RespostaApi CadastrarPessoa(PessoaDto pessoa)
        {
            return RespostaPadrao(_pessoaServico.IncluirPessoa(pessoa));
        }

        [HttpGet]
        [Route("Pessoas/[action]")]
        public RespostaApi<PessoaDto> ObterPessoa(string cpf)
        {
            return RespostaPadrao(_pessoaServico.PesquisarPessoaCpf(cpf));
        }

        [HttpPut]
        [Route("Pessoas/[action]")]
        public RespostaApi AtualizarDadosPessoa(PessoaDto pessoa)
        {
            return RespostaPadrao(_pessoaServico.AtualizarDadosPessoa(pessoa));
        }

        [HttpDelete]
        [Route("Pessoas/[action]")]
        public RespostaApi ExcluirPessoa(string nomePessoa)
        {
            return RespostaPadrao(_pessoaServico.ExcluirPessoa(nomePessoa));
        }
        #endregion

    }
}