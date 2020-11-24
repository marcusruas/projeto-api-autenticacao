namespace Abstracoes.Representacoes.Usuario.Usuario
{
    public class UsuarioAlteracaoSenhaDto
    {   
        public string SenhaAntiga { get; set; }
        public string SenhaNova { get; set; }
        public string ConfirmacaoSenha { get; set; }
    }
}