using System.Collections.Generic;
using AutoMapper;
using Abstracao.Representacao.Usuario.Grupo;
using Abstracao.Representacao.Usuario.Pessoa;
using MandradePkgs.Retornos;
using Microsoft.AspNetCore.Mvc;
using Servico.Usuario.Interface;
using SharedKernel.ObjetosValor.Enum;

namespace Api.Controllers.Usuarios
{
    [Route("Usuarios/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class PessoasController : ControllerApi
    {
        private IGrupoSrv _grupoServico { get; }
        private IPessoaSrv _pessoaServico { get; }
        private IMapper _mapper { get; }

        public PessoasController(IGrupoSrv grupoServico, IPessoaSrv pessoaServico, IMapper mapper)
        {
            _grupoServico = grupoServico;
            _pessoaServico = pessoaServico;
            _mapper = mapper;
        }

        [HttpPost]
        public RespostaApi CadastrarPessoa(PessoaDto pessoa)
        {
            return RespostaPadrao(_pessoaServico.IncluirPessoa(pessoa));
        }

        [HttpGet]
        public RespostaApi<PessoaDto> ObterPessoa(string cpf)
        {
            return RespostaPadrao(_pessoaServico.PesquisarPessoaCpf(cpf));
        }

        [HttpPut]
        public RespostaApi AtualizarDadosPessoa(PessoaDto pessoa)
        {
            return RespostaPadrao(_pessoaServico.AtualizarDadosPessoa(pessoa));
        }

        [HttpDelete]
        public RespostaApi ExcluirPessoa(string nomePessoa)
        {
            return RespostaPadrao(_pessoaServico.ExcluirPessoa(nomePessoa));
        }

    }
}