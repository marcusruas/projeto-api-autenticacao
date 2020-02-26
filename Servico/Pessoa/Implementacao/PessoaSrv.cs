﻿using AutoMapper;
using Dominio.Logica.Pessoa;
using Dominio.Representacoes.Pessoa;
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

        public PessoaSrv(IMensagensApi mensagens, IPessoaRep repositorio, IMapper mapper)
        {
            _mensagens = mensagens;
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public bool IncluirPessoa(PessoaDto pessoa)
        {
            var dominio = new PessoaDom(pessoa.Nome, pessoa.Cpf, pessoa.Email, pessoa.Telefone, _mensagens);

            dominio.ValidarNome();
            dominio.ValidarCpf();

            if (_mensagens.PossuiFalhasValidacao())
                throw new RegraNegocioException("Houve erros de validação. Favor verificar notificações.");

            var pessoaBanco = _mapper.Map<PessoaDpo>(pessoa);
            var sucesso = _repositorio.InserirPessoa(pessoaBanco);

            if (!sucesso)
                throw new RegraNegocioException("Já existe uma pessoa registrada para este CPF.");

            _mensagens.AdicionarMensagem("Pessoa adicionada com sucesso!");
            return sucesso;
        }

        public PessoaDto PesquisarPessoaCpf(string cpf)
        {
            var dominio = new PessoaDom(cpf, _mensagens);

            dominio.ValidarCpf();

            if (_mensagens.PossuiFalhasValidacao())
                throw new RegraNegocioException("Houve erros de validação. Favor verificar notificações.");

            var pessoa = _repositorio.BuscarPessoaCpf(dominio.Cpf.ValorNumerico);

            if (pessoa == null)
                _mensagens.AdicionarMensagem(TipoMensagem.Informativo, "Nenhum usuário encontrato com este CPF");

            return _mapper.Map<PessoaDto>(pessoa);
        }

        public bool AtualizarDadosPessoa(PessoaDto pessoa)
        {
            var dominio = new PessoaDom(pessoa.Nome, pessoa.Cpf, pessoa.Email, pessoa.Telefone, _mensagens);

            dominio.ValidarNome();
            dominio.ValidarCpf();

            if (_mensagens.PossuiFalhasValidacao())
                throw new RegraNegocioException("Houve erros de validação. Favor verificar notificações.");

            var pessoaBanco = _mapper.Map<PessoaDpo>(pessoa);
            var sucesso = _repositorio.UpdateDadosPessoa(pessoaBanco);

            if (!sucesso)
                throw new RegraNegocioException("Não foi possível localizar a pessoa. Verificar informações.");

            _mensagens.AdicionarMensagem("Pessoa atualizada com sucesso!");
            return sucesso;
        }

        public bool ExcluirPessoa(string nomePessoa)
        {
            var sucesso = _repositorio.DeletarPessoa(nomePessoa);
            if (!sucesso)
                throw new FalhaExecucaoException("Não foi possível localizar a pessoa. Verifique o nome digitado e tente novamente.");

            _mensagens.AdicionarMensagem($"Pessoa {nomePessoa} foi excluída com sucesso!");
            return sucesso;
        }
    }
}
