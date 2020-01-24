using Aplicacao.Pessoa;
using AutoMapper;
using Dominio.Pessoa;
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
            var dominio = _mapper.Map<PessoaDom>(pessoa);

            if (!dominio.CpfValido)
                throw new RegraNegocioException("CPF informado invalido!");

            if (!dominio.NomeValido())
                throw new RegraNegocioException("Nome da pessoa inválido. Os sobrenomes devem começar com letras maiúsculas.");

            var pessoaBanco = _mapper.Map<PessoaDbo>(pessoa);
            var sucesso = _repositorio.InserirPessoa(pessoaBanco);

            if (!sucesso)
                throw new RegraNegocioException("Já existe uma pessoa registrada para este CPF.");

            _mensagens.AdicionarMensagem("Pessoa adicionada com sucesso!");
            return sucesso;
        }

        public PessoaDto PesquisarPessoaCpf(string cpf) {
            bool cpfValido = CpfHelper.ValidarCpf(cpf);
            if (!cpfValido)
                throw new RegraNegocioException("CPF informado invalido!");

            long cpfFormatado = CpfHelper.RemoverFormatacao(cpf);

            var pessoa = _repositorio.BuscarPessoaCpf(cpfFormatado);

            if (pessoa == null)
                _mensagens.AdicionarMensagem(TipoMensagem.Informativo, "Nenhum usuário encontrato com este CPF");

            return _mapper.Map<PessoaDto>(pessoa);
        }

        public bool AtualizarDadosPessoa(PessoaDto pessoa) {
            var dominio = _mapper.Map<PessoaDom>(pessoa);

            if (!dominio.CpfValido)
                throw new RegraNegocioException("CPF informado invalido!");

            if (!dominio.NomeValido())
                throw new RegraNegocioException("Nome da pessoa inválido. Os sobrenomes devem começar com letras maiúsculas.");

            var pessoaBanco = _mapper.Map<PessoaDbo>(pessoa);
            var sucesso = _repositorio.UpdateDadosPessoa(pessoaBanco);

            if (!sucesso)
                throw new RegraNegocioException("Não foi possível localizar a pessoa. Verificar informações.");

            _mensagens.AdicionarMensagem("Pessoa atualizada com sucesso!");
            return sucesso;
        }
    }
}
