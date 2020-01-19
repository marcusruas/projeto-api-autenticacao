namespace Dominio.Pessoa
{
    public class PessoaDom
    {
        public PessoaDom(int id, string nome, long cpf, string email, long telefone) {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            Email = email;
            Telefone = telefone;
        }

        public int Id { get; }
        public string Nome { get; }
        public long Cpf { get; set; }
        public string Email { get; set; }
        public long Telefone { get; set; } 
    }
}
