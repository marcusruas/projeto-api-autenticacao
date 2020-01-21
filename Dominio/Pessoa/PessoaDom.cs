namespace Dominio.Pessoa
{
    public class PessoaDom
    {
        public PessoaDom(string nome, long cpf, string email, long telefone) {
            Nome = nome;
            Cpf = cpf;
            Email = email;
            Telefone = telefone;
        }

        public string Nome { get; }
        public long Cpf { get; }
        public string Email { get; }
        public long Telefone { get; } 
    }
}
