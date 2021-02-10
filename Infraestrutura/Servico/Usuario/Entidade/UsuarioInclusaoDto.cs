namespace Infraestrutura.Servico.Usuario.Entidade
{
    public class UsuarioInclusaoDto
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string ConfirmacaoSenha { get; set; }
        public int IdPessoa { get; set; }
        public int IdGrupo { get; set; }
    }
}