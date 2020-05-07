namespace Abstracoes.Representacoes.Usuario.Usuario
{
    public class UsuarioAlteracaoSenhaDto
    {
        public int Id { get; set; }
        public string SenhaAntiga { get; set; }
        public string SenhaNova { get; set; }
        public string ConfirmacaoSenha { get; set; }
    }
}