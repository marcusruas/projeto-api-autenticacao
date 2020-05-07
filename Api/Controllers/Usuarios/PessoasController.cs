using System.Collections.Generic;
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
        public RespostaApi CadastrarPessoa(PessoaInclusaoDto pessoa)
        {
            return RespostaPadrao(_pessoaServico.IncluirPessoa(pessoa));
        }

        [HttpPost]
        public RespostaApi<List<PessoaDto>> PesquisarPessoas(FiltroBuscaPessoasDto filtro)
        {
            return RespostaPadrao(_pessoaServico.PesquisarPessoas(filtro));
        }

        [HttpPut]
        public RespostaApi AtualizarDadosPessoa(PessoaDto pessoa)
        {
            return RespostaPadrao(_pessoaServico.AtualizarDadosPessoa(pessoa));
        }

        [HttpDelete]
        public RespostaApi ExcluirPessoa(int idPessoa)
        {
            return RespostaPadrao(_pessoaServico.ExcluirPessoa(idPessoa));
        }

    }
}