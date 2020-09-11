using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace Aplicacao.Representacoes.Autenticacao
{
    public class AssinaturaTokenDto
    {
        public AssinaturaTokenDto()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }
        }
        public SecurityKey Key { get; }
    }
}