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
using MandradePkgs.Autenticacao.Estrutura.Token;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Infraestrutura.Servico.Usuario.Implementacao
{
    public class UsuarioSv : IUsuarioSv
    {
        private IUsuarioRp _UsuarioRepositorio;
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
            _UsuarioRepositorio = _usuario;
            _grupoServico = _grupo;
            _pessoaServico = _pessoa;
            _mensagens = mensagens;
        }

        public bool AtualizarAtividadeUsuario(int id, bool ativo)
        {
            var resultado = _UsuarioRepositorio.AtualizarAtivoUsuario(id, ativo);

            if (resultado)
                _mensagens.AdicionarMensagem(MensagensErro.UsuarioSucessoAtualizacao);

            return resultado;
        }

        public bool AtualizarSenhaUsuario(int id, UsuarioAlteracaoSenhaDto alteracao)
        {
            var resultado = _UsuarioRepositorio.AtualizarSenhaUsuario(
                id,
                alteracao.SenhaAntiga,
                alteracao.SenhaNova
            );

            if (resultado)
                _mensagens.AdicionarMensagem(MensagensErro.UsuarioSucessoAtualizacao);

            return resultado;
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

            var dominio = new UsuarioDm(
                0, 
                usuario.Usuario, 
                usuario.Senha, 
                dataAtual, 
                true, 
                dataAtual, 
                usuario.DiasRenovacao,
                grupoDominio, 
                pessoaDominio
            );
            dominio.ValidarDados();

            if (_mensagens.PossuiFalhasValidacao())
                throw new RegraNegocioException(MensagensErro.RegraNegocioErroValidacao);

            var usuarioBanco = new UsuarioDpo(
                dominio.Id, 
                dominio.Usuario, 
                dominio.Senha.ValorCriptografado, 
                dataAtual, 
                dataAtual,
                dominio.DiasRenovacao,
                dominio.Ativo,
                dominio.Grupo.Id, 
                dominio.Pessoa.Id
            );

            var sucessoInsercao = _UsuarioRepositorio.InserirUsuario(usuarioBanco);

            if (!sucessoInsercao)
                _mensagens.AdicionarMensagem(TipoMensagem.Erro, MensagensErro.UsuarioFalhaInclusao);

            _mensagens.AdicionarMensagem(MensagensErro.UsuarioSucessoInclusao);
            return sucessoInsercao;
        }

        public GrupoDto ObterGrupoUsuario(int id)
        {
            var grupo = _UsuarioRepositorio.BuscarGrupoUsuario(id);

            if (grupo == null)
            {
                _mensagens.AdicionarMensagem(MensagensErro.UsuarioGrupoNaoEncontrado);
                return null;
            }

            return new GrupoDto(grupo);
        }

        public PessoaDto ObterPessoaUsuario(int id)
        {
            var pessoa = _UsuarioRepositorio.BuscarPessoaUsuario(id);

            if (pessoa == null)
            {
                _mensagens.AdicionarMensagem(MensagensErro.UsuarioPessoaNaoEncontrada);
                return null;
            }

            return new PessoaDto(pessoa);
        }

        public UsuarioDto PesquisarUsuario(int usuario)
        {
            var usuarioBanco = _UsuarioRepositorio.BuscarUsuario(usuario);
            
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

            var (usuarioBanco, grupo, pessoa) = _UsuarioRepositorio.BuscarUsuario(usuario, senhaCriptografada);
            return new UsuarioDto(usuarioBanco, grupo, pessoa);
        }

        public Token Autenticar(string usuario, string senha, ConfiguracoesToken configsToken, AssinaturaToken assinatura)
        {
            var usuarioBanco = ValidarUsuario(usuario, senha);

            if (usuarioBanco == null)
                throw new RegraNegocioException("N�o foi poss�vel localizar o usu�rio. Verifique os dados informados e tente novamente.");

            int usuarioAtivo = usuarioBanco.Ativo ? 1 : 0;

            ClaimsIdentity identity = new ClaimsIdentity(
                new[] {
                    new Claim("Usuario", usuarioBanco.Id.ToString()),
                    new Claim("Pessoa", usuarioBanco.Pessoa.Id.ToString()),
                    new Claim("Grupo", usuarioBanco.Grupo.Id.ToString()),
                }
            );

            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = dataCriacao + TimeSpan.FromMinutes(configsToken.DuracaoMinutos);

            var handler = new JwtSecurityTokenHandler();
            var dadosToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = configsToken.Originador,
                SigningCredentials = assinatura.credenciais,
                Subject = identity,
                NotBefore = dataCriacao,
                Expires = dataExpiracao
            });
            var token = handler.WriteToken(dadosToken);

            return new Token(token, dataCriacao, dataExpiracao);
        }
    }
}