using AutoMapper;
using Dominio.Logica.Usuario;
using Abstracoes.Representacoes.Usuario.Pessoa;
using MandradePkgs.Mensagens;
using MandradePkgs.Retornos.Erros.Exceptions;
using Repositorios.Usuario.Interfaces;
using Servicos.Usuario.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Abstracoes.Builders.Usuario;

namespace Servicos.Usuario.Implementacoes
{
    public class PessoaSrv : IPessoaSrv
    {
        private IMensagensApi _mensagens { get; }
        private IPessoaRep _Repositorio { get; }

        public PessoaSrv(IMensagensApi mensagens, IPessoaRep Repositorios)
        {
            _mensagens = mensagens;
            _Repositorio = Repositorios;
        }

        public bool IncluirPessoa(PessoaInclusaoDto pessoa)
        {
            var construtorDominio = new PessoaBuilder()
                .ConstruirObjeto(pessoa)
                .AdicionarMensageria(_mensagens);
            PessoaDom dominio = construtorDominio.Construir();
            PessoaDto dto = new PessoaDto(dominio);
            dominio.ValidarDados();

            if (_mensagens.PossuiFalhasValidacao())
                throw new RegraNegocioException("Houve erros de validação. Favor verificar notificações.");

            var pessoaBanco = new PessoaDpo(dto);
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
            pessoas.ForEach(pessoa => listaRetorno.Add(new PessoaDto(pessoa)));

            return listaRetorno;
        }

        public bool AtualizarDadosPessoa(PessoaAlteracaoDto pessoa)
        {
            if (!pessoa.possuiDadosAlteracao())
                throw new ArgumentException("Para realizar a alteração da pessoa, ao menos um dado deve ser alterado.");

            if (pessoa.Id == 0)
                throw new RegraNegocioException("Para realizar a alteração da pessoa, deve ser informado o número de identificação da mesma.");

            var construtorDominio = new PessoaBuilder()
                .ConstruirObjeto(pessoa)
                .AdicionarMensageria(_mensagens);
            PessoaDom dominio = construtorDominio.Construir();

            if (!string.IsNullOrWhiteSpace(pessoa.Nome))
                dominio.ValidarNome();

            dominio.ValidarDadosContato();
            dominio.ValidarCpf();

            if (_mensagens.PossuiFalhasValidacao())
                throw new RegraNegocioException("Houve erros de validação. Favor verificar notificações.");

            var pessoaBanco = new PessoaDpo(pessoa);
            var sucesso = _Repositorio.UpdateDadosPessoa(pessoaBanco);

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
    }
}
