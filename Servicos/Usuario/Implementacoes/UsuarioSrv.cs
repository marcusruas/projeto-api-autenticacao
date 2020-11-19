using System;
using Abstracoes.Builders.Usuario;
using Abstracoes.Representacoes.Usuario.Grupo;
using Abstracoes.Representacoes.Usuario.Pessoa;
using Abstracoes.Representacoes.Usuario.Usuario;
using Dominio.Logica.Usuario;
using MandradePkgs.Mensagens;
using MandradePkgs.Retornos.Erros.Exceptions;
using Repositorios.Usuario.Interfaces;
using Servicos.Usuario.Interfaces;
using SharedKernel.ObjetosValor.Formatos;

namespace Servicos.Usuario.Implementacoes
{
    public class UsuarioSrv : IUsuarioSrv
    {
        private IUsuarioRep _usuarioRepositorio;
        private IGrupoSrv _grupoServico;
        private IPessoaSrv _pessoaServico;
        private IMensagensApi _mensagens;

        public UsuarioSrv(
            IUsuarioRep _usuario,
            IGrupoSrv _grupo,
            IPessoaSrv _pessoa,
            IMensagensApi mensagens
        )
        {
            _usuarioRepositorio = _usuario;
            _grupoServico = _grupo;
            _pessoaServico = _pessoa;
            _mensagens = mensagens;
        }

        public bool AtualizarAtividadeUsuario(int id, bool ativo)
        {
            var resultado = _usuarioRepositorio.AtualizarAtivoUsuario(id, ativo);

            if (resultado)
                _mensagens.AdicionarMensagem("Usuário foi atualizado com sucesso!");

            return resultado;
        }

        public bool AtualizarSenhaUsuario(UsuarioAlteracaoSenhaDto alteracao)
        {
            var resultado = _usuarioRepositorio.AtualizarSenhaUsuario(
                alteracao.Id,
                alteracao.SenhaAntiga,
                alteracao.SenhaNova
            );

            if (resultado)
                _mensagens.AdicionarMensagem("Usuário foi atualizado com sucesso!");

            return resultado;
        }

        public bool ExcluirUsuario(int id)
        {
            var sucesso = _usuarioRepositorio.DeletarUsuario(id);
            if (!sucesso)
                throw new FalhaExecucaoException("Não foi possível localizar o usuário. Verifique os dados e tente novamente.");

            _mensagens.AdicionarMensagem($"Usuário foi excluído com sucesso!");
            return sucesso;
        }

        public bool IncluirUsuario(UsuarioInclusaoDto usuario)
        {
            if (usuario.Senha != usuario.ConfirmacaoSenha)
                throw new ArgumentException("Senha e confirmação de senha não são iguais, verifique os dados");

            GrupoDto grupo = _grupoServico.PesquisarGrupoPorId(usuario.IdGrupo);
            if (grupo == null)
                throw new ArgumentException("Grupo informado para o usuário não encontrado");

            var construtorDominioGrupo = new GrupoBuilder()
                .ConstruirObjeto(grupo)
                .AdicionarMensageria(_mensagens);
            var grupoDom = construtorDominioGrupo.Construir();

            var pessoa = _pessoaServico.PesquisarPessoaPorId(usuario.IdPessoa);
            if (pessoa == null)
                throw new ArgumentException("Pessoa informada para o usuário não encontrada");

            var construtorDominioPessoa = new PessoaBuilder()
                .ConstruirObjeto(pessoa)
                .AdicionarMensageria(_mensagens);
            PessoaDom pessoaDom = construtorDominioPessoa.Construir();

            var construtorDominioUsuario = new UsuarioBuilder()
                .ConstruirObjeto(usuario, grupoDom, pessoaDom)
                .AdicionarMensageria(_mensagens);
            UsuarioDom usuarioDom = construtorDominioUsuario.Construir();

            usuarioDom.ValidarDados();

            if (_mensagens.PossuiFalhasValidacao())
                throw new RegraNegocioException("Houve erros de validação. Favor verificar notificações.");

            var usuarioBanco = new UsuarioDpo(usuarioDom);
            var sucessoInsercao = _usuarioRepositorio.InserirUsuario(usuarioBanco);

            if (!sucessoInsercao)
                _mensagens.AdicionarMensagem(TipoMensagem.Erro, "Não foi possível adicionar o novo usuário. Tente novamente mais tarde.");

            _mensagens.AdicionarMensagem("Usuário Adicionado com sucesso!");
            return sucessoInsercao;
        }

        public GrupoDto ObterGrupoUsuario(int id)
        {
            var grupo = _usuarioRepositorio.BuscarGrupoUsuario(id);

            if (grupo == null)
            {
                _mensagens.AdicionarMensagem("Não foi possível localizar o usuário.");
                return null;
            }

            return new GrupoDto(grupo);
        }

        public PessoaDto ObterPessoaUsuario(int id)
        {
            var pessoa = _usuarioRepositorio.BuscarPessoaUsuario(id);

            if (pessoa == null)
            {
                _mensagens.AdicionarMensagem("Não foi possível localizar o usuário.");
                return null;
            }

            return new PessoaDto(pessoa);
        }

        public UsuarioDto PesquisarUsuario(int usuario)
        {
            var usuarioBanco = _usuarioRepositorio.BuscarUsuario(usuario);
            
            if (usuarioBanco == null)
            {
                _mensagens.AdicionarMensagem("Não foi possível localizar o usuário.");
                return null;
            }

            return usuarioBanco;
        }

        public UsuarioDto ValidarUsuario(string usuario, string senha)
        {
            string senhaCriptografada = new Senha(senha).ValorCriptografado;

            var (usuarioBanco, grupo, pessoa) = _usuarioRepositorio.BuscarUsuario(usuario, senhaCriptografada);
            return new UsuarioDto(usuarioBanco, grupo, pessoa);
        }
    }
}