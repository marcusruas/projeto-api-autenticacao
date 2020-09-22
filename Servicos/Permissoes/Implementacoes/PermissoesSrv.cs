using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Aplicacao.Representacoes.Usuario;
using MandradePkgs.Mensagens;
using MandradePkgs.Retornos.Erros.Exceptions;
using Microsoft.IdentityModel.Tokens;
using Servicos.Permissoes.Interfaces;
using Servicos.Usuario.Interfaces;

namespace Servicos.Permissoes.Implementacoes
{
    public class PermissoesSrv : IPermissoesSrv
    {
        private IUsuarioSrv _usuarioServico;
        private IMensagensApi _mensagens;

        public PermissoesSrv(IUsuarioSrv _usuario, IMensagensApi mensagens)
        {
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
    }
}