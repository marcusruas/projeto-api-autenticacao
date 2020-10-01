using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Abstracoes.Representacoes.Permissoes.Permissao;
using Aplicacao.Representacoes.Permissoes.Token;
using Logica.Permissoes;
using MandradePkgs.Mensagens;
using MandradePkgs.Retornos.Erros.Exceptions;
using Microsoft.IdentityModel.Tokens;
using Repositorios.Permissoes.Interfaces;
using Servicos.Permissoes.Interfaces;
using Servicos.Usuario.Interfaces;

namespace Servicos.Permissoes.Implementacoes
{
    public class PermissoesSrv : IPermissoesSrv
    {
        private IPermissoesRep _repositorio;
        private IUsuarioSrv _usuarioServico;
        private IMensagensApi _mensagens;

        public PermissoesSrv(IPermissoesRep repositorio, IUsuarioSrv _usuario, IMensagensApi mensagens)
        {
            _repositorio = repositorio;
            _usuarioServico = _usuario;
            _mensagens = mensagens;
        }

        public TokenDto Autenticar(string usuario, string senha, ConfiguracoesTokenDto configsToken, AssinaturaTokenDto assinatura)
        {
            var usuarioBanco = _usuarioServico.ValidarUsuario(usuario, senha);

            if (usuarioBanco == null)
                throw new RegraNegocioException("Não foi possível localizar o usuário. Verifique os dados informados e tente novamente.");

            ClaimsIdentity identity = new ClaimsIdentity(
                new[] {
                    new Claim("Usuario", usuarioBanco.Usuario),
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

            return new TokenDto(token, dataCriacao, dataExpiracao);
        }

        public PermissaoDto IncluirPermissao(string descricao)
        {
            var dominio = new PermissaoDom(descricao);
            bool sucesso = false;
            try
            {
                sucesso = _repositorio.InserirPermissao(new PermissaoDpo(dominio));
            }
            catch (SqlException ex)
            {
                _mensagens.AdicionarMensagem(ex.Message);
            }

            if (sucesso)
            {
                _mensagens.AdicionarMensagem(TipoMensagem.Informativo, "Permissão adicionada com sucesso!");
                return new PermissaoDto(dominio);
            }
            else
            {
                _mensagens.AdicionarMensagem(TipoMensagem.Erro, "Falha ao adicionar permissão, verifique os dados e tente novamente.");
                return null;
            }
        }

        public AcessoSistemicoDto IncluirAcesso(InclusaoAcessoSistemicoDto parametros)
        {
            List<PermissaoDto> permissoes = PesquisarPermissoes(parametros.Permissoes);
            List<int> permissoesNaoEncontradas = parametros.Permissoes.Where(p => !permissoes.Any(pp => pp.Id == p)).ToList();

            if (!permissoes.Any())
            {
                _mensagens.AdicionarMensagem("Não foi encontrada nenhuma permissão indicada, portanto a criação do acesso foi negada");
                return null;
            }

            foreach (var permissao in permissoesNaoEncontradas)
                _mensagens.AdicionarMensagem(
                    TipoMensagem.Alerta,
                    $"Não foi possível encontrar a permissão com Identificador {permissao}"
                );

            return null;
        }

        private List<PermissaoDto> PesquisarPermissoes(List<int> permissoes)
        {
            var listaPermissoes = _repositorio.PesquisarPermissoes(permissoes);
            List<PermissaoDto> retorno = new List<PermissaoDto>();

            foreach (var permissao in listaPermissoes)
                retorno.Add(new PermissaoDto(permissao));

            return retorno;
        }
    }
}