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
using System.Collections.Generic;
using System.Linq;

namespace Servicos.Usuario.Implementacoes
{
    public class PessoaSrv : IPessoaSrv
    {
        private IMensagensApi _mensagens { get; }
        private IPessoaRep _Repositorio { get; }
        private IPessoaTrd _tradutor { get; }

        public PessoaSrv(IMensagensApi mensagens, IPessoaRep Repositorios, IPessoaTrd tradutor)
        {
            _mensagens = mensagens;
            _Repositorio = Repositorios;
            _tradutor = tradutor;
        }

        public bool IncluirPessoa(PessoaDto pessoa)
        {
            var dominio = _tradutor.MapearParaDominio(pessoa, _mensagens);

            dominio.ValidarDados();

            if (_mensagens.PossuiFalhasValidacao())
                throw new RegraNegocioException("Houve erros de validação. Favor verificar notificações.");

            var pessoaBanco = _tradutor.MapearParaDpo(pessoa);
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

            var pessoas = _Repositorio.BuscarPessoas(filtro);

            if (pessoas.Count() == 0)
            {
                _mensagens.AdicionarMensagem(TipoMensagem.Informativo, "A consulta não retornou resultados");
                return new List<PessoaDto>();
            }

            var listaRetorno = new List<PessoaDto>();
            pessoas.ForEach(pessoa => listaRetorno.Add(_tradutor.MapearParaDto(pessoa)));

            return listaRetorno;
        }

        public bool AtualizarDadosPessoa(PessoaDto pessoa)
        {
            var dominio = _tradutor.MapearParaDominio(pessoa, _mensagens);

            if (pessoa.Id == 0)
                throw new RegraNegocioException("Para realizar a alteração da pessoa, deve ser informado o número de identificação da mesma.");

            if (string.IsNullOrWhiteSpace(pessoa.Nome) &&
                string.IsNullOrWhiteSpace(pessoa.Email) &&
                pessoa.Cpf == null && pessoa.Telefone == null)
                throw new ArgumentException("Para realizar a alteração da pessoa, ao menos um dado deve ser alterado.");

            if (!string.IsNullOrWhiteSpace(pessoa.Nome))
                dominio.ValidarNome();

            dominio.ValidarDadosContato();
            dominio.ValidarCpf();

            if (_mensagens.PossuiFalhasValidacao())
                throw new RegraNegocioException("Houve erros de validação. Favor verificar notificações.");

            var pessoaBanco = _tradutor.MapearParaDpo(pessoa);
            var sucesso = _Repositorio.UpdateDadosPessoa(pessoaBanco);

            if (!sucesso)
                throw new RegraNegocioException("Não foi possível atualizar a pessoa. Favor Verificar informações.");

            _mensagens.AdicionarMensagem("Pessoa atualizada com sucesso!");
            return sucesso;
        }

        public bool ExcluirPessoa(string nomePessoa)
        {
            var sucesso = _Repositorio.DeletarPessoa(nomePessoa);
            if (!sucesso)
                throw new FalhaExecucaoException("Não foi possível deletar a pessoa. Tente novamente mais tarde");

            _mensagens.AdicionarMensagem($"{nomePessoa} foi excluído(a) com sucesso!");
            return sucesso;
        }

        public PessoaDto PesquisarPessoaPorId(int id) =>
            _tradutor.MapearParaDto(_Repositorio.ObterPessoaPorId(id));
    }
}
