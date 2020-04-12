using Abstracoes.Representacoes.Usuario.Pessoa;
using AutoMapper;
using MandradePkgs.Retornos;
using Microsoft.AspNetCore.Mvc;
using Servicos.Usuario.Interfaces;

namespace Api.Controllers.Usuarios
{
    [Route("Usuarios/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class PessoasController : ControllerApi
    {
        private IPessoaSrv _pessoaServico { get; }

        public PessoasController(IPessoaSrv pessoaServico)
        {
            _pessoaServico = pessoaServico;
        }

        [HttpPost]
        public RespostaApi CadastrarPessoa(PessoaDto pessoa)
        {
            return RespostaPadrao(_pessoaServico.IncluirPessoa(pessoa));
        }

        [HttpGet]
        public RespostaApi<PessoaDto> BuscarPessoaPorCpf(string cpf)
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