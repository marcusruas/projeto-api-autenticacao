using MandradePkgs.Mensagens;
using MandradePkgs.Retornos.Erros.Exceptions;
using Infraestrutura.Repositorio.Usuario.Interface;
using Infraestrutura.Servico.Usuario.Interface;
using Infraestrutura.Servico.Usuario.Entidade;
using System;
using Dominio.Entidade.Usuario;
using Infraestrutura.Repositorio.Entidade;
using Dominio.ObjetoValor.Formatos;
using Servico.Recurso;

namespace Infraestrutura.Servico.Usuario.Implementacao
{
    public class UsuarioSv : IUsuarioSv
    {
        private IUsuarioRp _UsuarioRpositorio;
        private IGrupoSv _grupoServico;
        private IPessoaSv _pessoaServico;
        private IMensagensApi _mensagens;

        public UsuarioSv(
            IUsuarioRp _usuario,
            IGrupoSv _grupo,
            IPessoaSv _pessoa,
            IMensagensApi mensagens
        )
        {
            _UsuarioRpositorio = _usuario;
            _grupoServico = _grupo;
            _pessoaServico = _pessoa;
            _mensagens = mensagens;
        }

        public bool AtualizarAtividadeUsuario(int id, bool ativo)
        {
            var resultado = _UsuarioRpositorio.AtualizarAtivoUsuario(id, ativo);

            if (resultado)
                _mensagens.AdicionarMensagem(MensagensErro.UsuarioSucessoAtualizacao);

            return resultado;
        }

        public bool AtualizarSenhaUsuario(int id, UsuarioAlteracaoSenhaDto alteracao)
        {
            var resultado = _UsuarioRpositorio.AtualizarSenhaUsuario(
                id,
                alteracao.SenhaAntiga,
                alteracao.SenhaNova
            );

            if (resultado)
                _mensagens.AdicionarMensagem(MensagensErro.UsuarioSucessoAtualizacao);

            return resultado;
        }

        public bool ExcluirUsuario(int id)
        {
            var sucesso = _UsuarioRpositorio.DeletarUsuario(id);
            if (!sucesso)
                throw new FalhaExecucaoException(MensagensErro.UsuarioNaoLocalizado);

            _mensagens.AdicionarMensagem(MensagensErro.UsuarioSucessoExclusao);
            return sucesso;
        }

        public bool IncluirUsuario(UsuarioInclusaoDto usuario)
        {
            if (usuario.Senha != usuario.ConfirmacaoSenha)
                throw new ArgumentException(MensagensErro.UsuarioFalhaConfirmacaoSenha);

            DateTime dataAtual = DateTime.Now;

            GrupoDto grupo = _grupoServico.PesquisarGrupoPorId(usuario.IdGrupo);
            if (grupo == null)
                throw new ArgumentException(MensagensErro.UsuarioGrupoNaoEncontrado);

            var grupoDominio = new GrupoDm(grupo.Id, grupo.Nome, grupo.Descricao, grupo.Pai);
            grupoDominio.DefinirMensagens(_mensagens);

            var pessoa = _pessoaServico.PesquisarPessoaPorId(usuario.IdPessoa);
            if (pessoa == null)
                throw new ArgumentException(MensagensErro.UsuarioPessoaNaoEncontrada);

            var pessoaDominio = new PessoaDm(pessoa.Id, pessoa.Nome, pessoa.Cpf, pessoa.Email, pessoa.Telefone);
            pessoaDominio.DefinirMensagens(_mensagens);

            var dominio = new UsuarioDm(0, usuario.Usuario, usuario.Senha, dataAtual, true, grupoDominio, pessoaDominio);
            dominio.ValidarDados();

            if (_mensagens.PossuiFalhasValidacao())
                throw new RegraNegocioException(MensagensErro.RegraNegocioErroValidacao);

            var usuarioBanco = new UsuarioDpo(dominio.Id, dominio.Usuario, dominio.Senha.Valor, dataAtual, dominio.Ativo, dominio.Grupo.Id, dominio.Pessoa.Id);
            var sucessoInsercao = _UsuarioRpositorio.InserirUsuario(usuarioBanco);

            if (!sucessoInsercao)
                _mensagens.AdicionarMensagem(TipoMensagem.Erro, MensagensErro.UsuarioFalhaInclusao);

            _mensagens.AdicionarMensagem(MensagensErro.UsuarioSucessoInclusao);
            return sucessoInsercao;
        }

        public GrupoDto ObterGrupoUsuario(int id)
        {
            var grupo = _UsuarioRpositorio.BuscarGrupoUsuario(id);

            if (grupo == null)
            {
                _mensagens.AdicionarMensagem(MensagensErro.UsuarioGrupoNaoEncontrado);
                return null;
            }

            return new GrupoDto(grupo);
        }

        public PessoaDto ObterPessoaUsuario(int id)
        {
            var pessoa = _UsuarioRpositorio.BuscarPessoaUsuario(id);

            if (pessoa == null)
            {
                _mensagens.AdicionarMensagem(MensagensErro.UsuarioPessoaNaoEncontrada);
                return null;
            }

            return new PessoaDto(pessoa);
        }

        public UsuarioDto PesquisarUsuario(int usuario)
        {
            var usuarioBanco = _UsuarioRpositorio.BuscarUsuario(usuario);
            
            if (usuarioBanco.Item1 == null)
            {
                _mensagens.AdicionarMensagem(MensagensErro.UsuarioNaoLocalizado);
                return null;
            }

            return new UsuarioDto(usuarioBanco.Item1, usuarioBanco.Item2, usuarioBanco.Item3);
        }

        public UsuarioDto ValidarUsuario(string usuario, string senha)
        {
            string senhaCriptografada = new Senha(senha).ValorCriptografado;

            var (usuarioBanco, grupo, pessoa) = _UsuarioRpositorio.BuscarUsuario(usuario, senhaCriptografada);
            return new UsuarioDto(usuarioBanco, grupo, pessoa);
        }
    }
}