using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace Aplicacao.Representacoes.Permissoes.Token
{
    public class AssinaturaTokenDto
    {
        public AssinaturaTokenDto()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            credenciais = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        }
        public SecurityKey Key { get; }
        public SigningCredentials credenciais { get; }
    }
}