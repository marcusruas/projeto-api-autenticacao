namespace Aplicacao.Representacoes.Autenticacao
{
    public class ConfiguracoesTokenDto
    {
        public string Originador { get; set; }
        public string Audience { get; set; }
        public int DuracaoMinutos { get; set; }
    }
}