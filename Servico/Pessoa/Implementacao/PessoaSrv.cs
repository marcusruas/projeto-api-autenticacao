using Aplicacao.Pessoa;
using AutoMapper;
using Helpers;
using MandradePkgs.Mensagens;
using MandradePkgs.Retornos.Erros.Exceptions;
using Repositorio.Pessoa.Interface;
using Servico.Pessoa.Interface;
using System;

namespace Servico.Pessoa.Implementacao
{
    public class PessoaSrv : IPessoaSrv
    {
        private IMensagensApi _mensagens { get; }
        private IPessoaRep _repositorio { get; }
        private IMapper _mapper { get; }

        public PessoaSrv(IMensagensApi mensagens, IPessoaRep repositorio, IMapper mapper) {
            _mensagens = mensagens;
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public bool IncluirPessoa(PessoaDto pessoa) {
            bool cpfValido = CpfFormat.ValidarCpf(pessoa.Cpf.ToString());

            if (!cpfValido)
                throw new RegraNegocioException("CPF informado invalido!");

            if (string.IsNullOrWhiteSpace(pessoa.Nome))
                throw new RegraNegocioException("Necessário informar o nome da pessoa");

            var pessoaBanco = _mapper.Map<PessoaDbo>(pessoa);
            var sucesso = _repositorio.InserirPessoa(pessoaBanco);

            if (!sucesso)
                throw new RegraNegocioException("Já existe uma pessoa registrada para este CPF.");

            _mensagens.AdicionarMensagem("Pessoa adicionada com sucesso!");
            return sucesso;
        }
    }
}
