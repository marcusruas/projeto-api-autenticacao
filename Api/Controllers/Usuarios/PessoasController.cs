using System.Collections.Generic;
using Abstracoes.Representacoes.Usuario.Pessoa;
using AutoMapper;
using MandradePkgs.Retornos;
using Microsoft.AspNetCore.Mvc;
using Servicos.Usuario.Interfaces;

namespace Api.Controllers.usuarios
{
    [Produces("application/json")]
    [ApiController]
    public class PessoasController : ControllerApi
    {
        private IPessoaSrv _pessoaServico { get; }

        public PessoasController(IPessoaSrv pessoaServico)
        {
            _pessoaServico = pessoaServico;
        }

        [HttpGet]
        [Route("/usuarios/pessoas/")]
        public RespostaApi<List<PessoaDto>> Pesquisar([FromQuery] FiltroBuscaPessoasDto filtro)
        {
            return RespostaPadrao(_pessoaServico.PesquisarPessoas(filtro));
        }

        [HttpGet]
        [Route("/usuarios/pessoas/{id}")]
        public RespostaApi<PessoaDto> Get(int id)
        {
            return RespostaPadrao(_pessoaServico.PesquisarPessoaPorId(id));
        }
        
        [HttpPost]
        [Route("/usuarios/pessoas/")]
        public RespostaApi Cadastrar(PessoaInclusaoDto pessoa)
        {
            return RespostaPadrao(_pessoaServico.IncluirPessoa(pessoa));
        }

        [HttpPut]
        [Route("/usuarios/pessoas/")]
        public RespostaApi Atualizar(PessoaDto pessoa)
        {
            return RespostaPadrao(_pessoaServico.AtualizarDadosPessoa(pessoa));
        }

        [HttpDelete]
        [Route("/usuarios/pessoas/{id}")]
        public RespostaApi Excluir(int id)
        {
            return RespostaPadrao(_pessoaServico.ExcluirPessoa(id));
        }

    }
}