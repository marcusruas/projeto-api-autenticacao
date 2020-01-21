using Aplicacao.Pessoa;
using AutoMapper;
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
            try {
                var pessoaBanco = _mapper.Map<PessoaDbo>(pessoa);

                var sucesso = _repositorio.InserirPessoa(pessoaBanco);

                if (!sucesso)
                    throw new RegraNegocioException("Já existe uma pessoa registrada com este CPF.");

                _mensagens.AdicionarMensagem("Pessoa adicionada com sucesso!");

                return sucesso;
            } catch (Exception ex) {
                _mensagens.AdicionarMensagem(TipoMensagem.Erro, ex.Message);
                throw new FalhaExecucaoException(ex.Message);
            }
        }
    }
}
