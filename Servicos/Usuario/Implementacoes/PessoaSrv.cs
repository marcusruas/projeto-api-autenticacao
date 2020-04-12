using AutoMapper;
using Dominio.Logica.Usuario;
using Abstracoes.Representacoes.Usuario.Pessoa;
using MandradePkgs.Mensagens;
using MandradePkgs.Retornos.Erros.Exceptions;
using Repositorios.Usuario.Interfaces;
using Servicos.Usuario.Interfaces;
using Abstracoes.Tradutores.Usuario.Interfaces;

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
                throw new RegraNegocioException("Já existe uma pessoa registrada para este CPF.");

            _mensagens.AdicionarMensagem("Pessoa adicionada com sucesso!");
            return sucesso;
        }

        public PessoaDto PesquisarPessoaCpf(string cpf)
        {
            var pessoa = _Repositorios.BuscarPessoaCpf(cpf);

            if (pessoa == null)
                _mensagens.AdicionarMensagem(TipoMensagem.Informativo, "Nenhum usuário encontrato com este CPF");

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
