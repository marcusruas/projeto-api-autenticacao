using AutoMapper;
using Dominio.Logica.Usuario;
using Abstracoes.Representacoes.Usuario.Pessoa;
using MandradePkgs.Mensagens;
using MandradePkgs.Retornos.Erros.Exceptions;
using Repositorios.Usuario.Interfaces;
using Servicos.Usuario.Interfaces;
using Abstracoes.Tradutores.Usuario.Interfaces;
using SharedKernel.ObjetosValor.Formatos;
using System;

namespace Servicos.Usuario.Implementacoes
{
    public class PessoaSrv : IPessoaSrv
    {
        private IMensagensApi _mensagens { get; }
        private IPessoaRep _Repositorios { get; }
        private IPessoaTrd _tradutor { get; }

        public PessoaSrv(IMensagensApi mensagens, IPessoaRep Repositorios, IPessoaTrd tradutor)
        {
            _mensagens = mensagens;
            _Repositorios = Repositorios;
            _tradutor = tradutor;
        }

        public bool IncluirPessoa(PessoaDto pessoa)
        {
            var dominio = _tradutor.MapearParaDominio(pessoa, _mensagens);

            dominio.ValidarDados();

            if (_mensagens.PossuiFalhasValidacao())
                throw new RegraNegocioException("Houve erros de validação. Favor verificar notificações.");

            var pessoaBanco = _tradutor.MapearParaDpo(pessoa);
            var sucesso = _Repositorios.InserirPessoa(pessoaBanco);

            if (!sucesso)
                throw new RegraNegocioException("Não foi possível incluir esta pessoa. Verifique os dados ou tente novamente mais tarde.");

            _mensagens.AdicionarMensagem("Pessoa adicionada com sucesso!");
            return sucesso;
        }

        public PessoaDto PesquisarPessoaCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                throw new ArgumentException("CPF não informado. Informe um CPF para poder realizar esta consulta");

            var cpfParametro = new Cpf(cpf);
            var pessoa = _Repositorios.BuscarPessoaCpf(cpfParametro);

            if (pessoa == null)
                throw new Exception("Nenhuma pessoa encontrada com este CPF");

            return _tradutor.MapearParaDto(pessoa);
        }

        public bool AtualizarDadosPessoa(PessoaDto pessoa)
        {
            var dominio = _tradutor.MapearParaDominio(pessoa, _mensagens);

            dominio.ValidarCpf();

            if (_mensagens.PossuiFalhasValidacao())
                throw new RegraNegocioException("Houve erros de validação. Favor verificar notificações.");

            var pessoaBanco = _tradutor.MapearParaDpo(pessoa);
            var sucesso = _Repositorios.UpdateDadosPessoa(pessoaBanco);

            if (!sucesso)
                throw new RegraNegocioException("Não foi possível localizar a pessoa. Verificar informações.");

            _mensagens.AdicionarMensagem("Pessoa atualizada com sucesso!");
            return sucesso;
        }

        public bool ExcluirPessoa(string nomePessoa)
        {
            var sucesso = _Repositorios.DeletarPessoa(nomePessoa);
            if (!sucesso)
                throw new FalhaExecucaoException("Não foi possível localizar a pessoa. Verifique o nome digitado e tente novamente.");

            _mensagens.AdicionarMensagem($"Pessoa {nomePessoa} foi excluída com sucesso!");
            return sucesso;
        }
    }
}
