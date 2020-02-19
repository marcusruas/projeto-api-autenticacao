using AutoMapper;
using MandradePkgs.Retornos;
using Microsoft.AspNetCore.Mvc;
using Aplicacao.Pessoa;
using Servico.Pessoa.Interface;

namespace Api.Controllers
{
    [Route("[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class PessoasController : ControllerApi
    {
        private IPessoaSrv _servico { get; }
        private IMapper _mapper { get; }

        public PessoasController(IPessoaSrv servico, IMapper mapper)
        {
            _servico = servico;
            _mapper = mapper;
        }

        [HttpPost]
        public RespostaApi CadastrarPessoa(PessoaDto pessoa)
        {
            return RespostaPadrao(_servico.IncluirPessoa(pessoa));
        }

        [HttpGet]
        public RespostaApi<PessoaDto> ObterPessoa(string cpf)
        {
            return RespostaPadrao(_servico.PesquisarPessoaCpf(cpf));
        }

        [HttpPut]
        public RespostaApi AtualizarDadosPessoa(PessoaDto pessoa)
        {
            return RespostaPadrao(_servico.AtualizarDadosPessoa(pessoa));
        }

        [HttpDelete]
        public RespostaApi ExcluirPessoa(string nomePessoa)
        {
            return RespostaPadrao(_servico.ExcluirPessoa(nomePessoa));
        }
    }
}