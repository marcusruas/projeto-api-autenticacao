using MandradePkgs.Mensagens;
using Infraestrutura.Repositorio.Usuario.Interface;
using Infraestrutura.Servicos.Usuario.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Infraestrutura.Servico.Usuario.Entidade;
using Dominio.Entidade.Usuario;
using Infraestrutura.Repositorio.Usuario.Entidade;
using MandradePkgs.Retornos.Erros.Exceptions;
using Dominio.ObjetoValor.Formatos;

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
                throw new RegraNegocioException("Houve erros de validação. Favor verificar notificações.");

            var pessoaBanco = new PessoaDpo(0, pessoa.Nome, pessoa.Cpf, pessoa.Email, pessoa.Ddd, pessoa.Numero);
            var sucesso = _Repositorio.InserirPessoa(pessoaBanco);

            if (!sucesso)
                throw new RegraNegocioException("Não foi possível incluir esta pessoa. Verifique os dados ou tente novamente mais tarde.");

            _mensagens.AdicionarMensagem("Pessoa adicionada com sucesso!");
            return sucesso;
        }

        public List<PessoaDto> PesquisarPessoas(FiltroBuscaPessoasDto filtro)
        {
            if (!filtro.PossuiCpf() && !filtro.PossuiNome())
                throw new ArgumentException("Ao menos um filtro deve ser preenchido.");

            var pessoas = _Repositorio.BuscarPessoas(filtro.nome, filtro.cpf);

            if (pessoas.Count() == 0)
            {
                _mensagens.AdicionarMensagem(TipoMensagem.Informativo, "A consulta não retornou resultados");
                return new List<PessoaDto>();
            }

            var listaRetorno = new List<PessoaDto>();
            pessoas.ForEach(pessoa => listaRetorno.Add(new PessoaDto(pessoa)));

            return listaRetorno;
        }

        public bool AtualizarDadosPessoa(PessoaAlteracaoDto pessoa)
        {
            if (!pessoa.PossuiSolicitacaoAlteracao())
                throw new ArgumentException("Para realizar a alteração da pessoa, ao menos um dado deve ser alterado.");

            if (pessoa.Dados.Id == 0)
                throw new RegraNegocioException("Para realizar a alteração da pessoa, deve ser informado o número de identificação da mesma.");

            var dadosAtuaisPessoa = _Repositorio.ObterPessoaPorId(pessoa.Dados.Id);

            string emailPessoa = pessoa.AlterarEmail ? pessoa.Dados.Email : dadosAtuaisPessoa.Email;
                
            var dominio = new PessoaDm(pessoa.Dados.Id, pessoa.Dados.Nome, null, emailPessoa, ValidarTelefone(dadosAtuaisPessoa, pessoa));
            dominio.DefinirMensagens(_mensagens);

            if (pessoa.AlterarNome)
                dominio.ValidarNome();
            dominio.ValidarDadosContato();

            if (_mensagens.PossuiFalhasValidacao())
                throw new RegraNegocioException("Houve erros de validação. Favor verificar notificações.");

            if (dadosAtuaisPessoa == null) {
                throw new FalhaExecucaoException("Não foi possível localizar a pessoa indicada para alteração de dados");
            }

            var pessoaAtualizacao = RealizarMergeInformacoes(dadosAtuaisPessoa, pessoa);
            var sucesso = _Repositorio.UpdateDadosPessoa(pessoaAtualizacao);

            if (!sucesso)
                throw new RegraNegocioException("Não foi possível atualizar a pessoa. Favor Verificar informações.");

            _mensagens.AdicionarMensagem("Pessoa atualizada com sucesso!");
            return sucesso;
        }

        public bool ExcluirPessoa(int id)
        {
            var sucesso = _Repositorio.DeletarPessoa(id);
            if (!sucesso)
                throw new FalhaExecucaoException("Não foi possível deletar a pessoa. Tente novamente mais tarde");

            _mensagens.AdicionarMensagem($"Pessoa foi excluída com sucesso!");
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
