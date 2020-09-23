using System;

namespace Aplicacao.Representacoes.Permissoes.Token
{
    public class TokenDto
    {
        public TokenDto(string tokenAcesso, DateTime dataCriacao, DateTime dataExpiracao)
        {
            this.TokenAcesso = tokenAcesso;
            this.DataCriacao = dataCriacao;
            this.DataExpiracao = dataExpiracao;
        }
        public string TokenAcesso { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataExpiracao { get; set; }
    }
}