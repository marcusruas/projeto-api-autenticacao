using System.Collections.Generic;
using Infraestrutura.Servico.Usuario.Entidade;
using Infraestrutura.Servico.Usuario.Interface;
using MandradePkgs.Retornos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.usuarios
{
    [Route("/usuarios/pessoas/")]
    [Produces("application/json")]
    [ApiController]
    public class PessoasController : ControllerApi
    {
        private IPessoaSv _pessoaServico { get; }

        public PessoasController(IPessoaSv pessoaServico)
        {
            _pessoaServico = pessoaServico;
        }

        [HttpGet]
        [Route("{id}")]
        public RespostaApi<PessoaDto> Get(int id)
        {
            return RespostaPadrao(_pessoaServico.PesquisarPessoaPorId(id));
        }
        
        [HttpPost]
        public RespostaApi Post(PessoaInclusaoDto pessoa)
        {
            return RespostaPadrao(_pessoaServico.IncluirPessoa(pessoa));
        }

        [HttpPut]
        public RespostaApi Put(PessoaAlteracaoDto pessoa)
        {
            return RespostaPadrao(_pessoaServico.AtualizarDadosPessoa(pessoa));
        }

        [HttpDelete]
        [Route("{id}")]
        public RespostaApi Delete(int id)
        {
            return RespostaPadrao(_pessoaServico.ExcluirPessoa(id));
        }

        [HttpGet]
        public RespostaApi<List<PessoaDto>> Pesquisar([FromQuery] FiltroBuscaPessoasDto filtro)
        {
            return RespostaPadrao(_pessoaServico.PesquisarPessoas(filtro));
        }
    }
}