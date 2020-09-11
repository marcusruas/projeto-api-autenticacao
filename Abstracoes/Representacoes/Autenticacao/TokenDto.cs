using System;

namespace Aplicacao.Representacoes.Autenticacao
{
    public class TokenDto
    {
        public string TokenAcesso { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataExpiracao { get; set; }
    }
}