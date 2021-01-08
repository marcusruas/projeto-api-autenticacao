using MandradePkgs.Mensagens;
using Infraestrutura.Repositorio.Usuario.Interface;
using Infraestrutura.Servico.Usuario.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Infraestrutura.Servico.Usuario.Entidade;
using Dominio.Entidade.Usuario;
using Infraestrutura.Repositorio.Usuario.Entidade;
using MandradePkgs.Retornos.Erros.Exceptions;
using Dominio.ObjetoValor.Formatos;
using Servico.Recurso;

namespace Infraestrutura.Servico.Usuario.Implementacao
{
    public class PessoaSv : IPessoaSv
    {
        private IMensagensApi _mensagens { get; }
        private IPessoaRp _Repositorio { get; }

        public PessoaSv(IMensagensApi mensagens, IPessoaRp Repositorios)
        {
            _mensagens = mensagens;
            _Repositorio = Repositorios;
        }

        public bool IncluirPessoa(PessoaInclusaoDto pessoa)
        {
            Telefone telefonePessoa = new Telefone(pessoa.Ddd, pessoa.Numero);
            Cpf cpfPessoa = new Cpf(pessoa.Cpf);

            var dominio = new PessoaDm(0, pessoa.Nome, cpfPessoa, pessoa.Email, telefonePessoa);
            dominio.DefinirMensagens(_mensagens);
            
            PessoaDto dto = new PessoaDto(dominio);
            dominio.ValidarDados();

            if (_mensagens.PossuiFalhasValidacao())
                throw new RegraNegocioException(MensagensErro.RegraNegocioErroValidacao);

            var pessoaBanco = new PessoaDpo(0, pessoa.Nome, pessoa.Cpf, pessoa.Email, pessoa.Ddd, pessoa.Numero);
            var sucesso = _Repositorio.InserirPessoa(pessoaBanco);

            if (!sucesso)
                throw new RegraNegocioException(MensagensErro.PessoaFalhaInclusao);

            _mensagens.AdicionarMensagem(MensagensErro.PessoaSucessoInclusao);
            return sucesso;
        }

        public List<PessoaDto> PesquisarPessoas(FiltroBuscaPessoasDto filtro)
        {
            var pessoas = _Repositorio.BuscarPessoas(filtro.nome, filtro.cpf);

            if (!pessoas.Any())
            {
                _mensagens.AdicionarMensagem(TipoMensagem.Informativo, MensagensErro.PesquisaSemResultados);
                return new List<PessoaDto>();
            }

            var listaRetorno = new List<PessoaDto>();
            pessoas.ForEach(pessoa => listaRetorno.Add(new PessoaDto(pessoa)));

            return listaRetorno;
        }

        public bool AtualizarDadosPessoa(PessoaAlteracaoDto pessoa)
        {
            if (!pessoa.PossuiSolicitacaoAlteracao())
                throw new ArgumentException(MensagensErro.PessoaAlteracaoSemDados);

            if (pessoa.Dados.Id == 0)
                throw new RegraNegocioException(MensagensErro.PessoaAlteracaoSemId);

            var dadosAtuaisPessoa = _Repositorio.ObterPessoaPorId(pessoa.Dados.Id);

            string emailPessoa = pessoa.AlterarEmail ? pessoa.Dados.Email : dadosAtuaisPessoa.Email;
            var dominio = new PessoaDm(pessoa.Dados.Id, pessoa.Dados.Nome, null, emailPessoa, ValidarTelefone(dadosAtuaisPessoa, pessoa));
            dominio.DefinirMensagens(_mensagens);

            if (pessoa.AlterarNome)
                dominio.ValidarNome();
            dominio.ValidarDadosContato();

            if (_mensagens.PossuiFalhasValidacao())
                throw new RegraNegocioException(MensagensErro.RegraNegocioErroValidacao);

            if (dadosAtuaisPessoa == null) {
                throw new FalhaExecucaoException(MensagensErro.PessoaAlteracaoNaoEncontrada);
            }

            var pessoaAtualizacao = RealizarMergeInformacoes(dadosAtuaisPessoa, pessoa);
            var sucesso = _Repositorio.UpdateDadosPessoa(pessoaAtualizacao);

            if (!sucesso)
                throw new RegraNegocioException(MensagensErro.PessoaFalhaAlteracao);

            _mensagens.AdicionarMensagem(MensagensErro.PessoaAlteracaoSucesso);
            return sucesso;
        }

        public bool ExcluirPessoa(int id)
        {
            var sucesso = _Repositorio.DeletarPessoa(id);
            if (!sucesso)
                throw new FalhaExecucaoException(MensagensErro.PessoaFalhaExclusao);

            _mensagens.AdicionarMensagem(MensagensErro.PessoaSucessoExclusao);
            return sucesso;
        }

        public PessoaDto PesquisarPessoaPorId(int id)
        {
            var pessoa = _Repositorio.ObterPessoaPorId(id);
            if (pessoa == null)
                return null;
            return new PessoaDto(pessoa);
        }

        private PessoaDpo RealizarMergeInformacoes(PessoaDpo pessoa, PessoaAlteracaoDto alteracao) {
            string nomeNovo = alteracao.AlterarNome ? alteracao.Dados.Nome : pessoa.Nome;
            string emailNovo = alteracao.AlterarEmail ? alteracao.Dados.Email : pessoa.Email;
            string dddNovo = alteracao.AlterarTelefone ? alteracao.Dados.DddTelefone : pessoa.Ddd;
            string numeroNovo = alteracao.AlterarTelefone ? alteracao.Dados.NumeroTelefone : pessoa.Numero;

            return new PessoaDpo(
                alteracao.Dados.Id,
                nomeNovo,
                null,
                emailNovo,
                dddNovo,
                numeroNovo
            );
        }

        private Telefone ValidarTelefone(PessoaDpo pessoa, PessoaAlteracaoDto alteracao) {
            Telefone telefonePessoa = null;
            if(alteracao.AlterarTelefone) {
                if(!string.IsNullOrWhiteSpace(alteracao.Dados.DddTelefone) && !string.IsNullOrWhiteSpace(alteracao.Dados.NumeroTelefone))
                    telefonePessoa = new Telefone(alteracao.Dados.DddTelefone, alteracao.Dados.NumeroTelefone);
            }
            else if(!string.IsNullOrWhiteSpace(pessoa.Ddd) && !string.IsNullOrWhiteSpace(pessoa.Numero))
                telefonePessoa = new Telefone(pessoa.Ddd, pessoa.Numero);
            
            return telefonePessoa;
        }
    }
}   
