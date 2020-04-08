using SharedKernel.ObjetosValor.Formatos;

namespace Abstracoes.Representacoes.Usuario.Pessoa
{
    public class PessoaDto
    {
        public PessoaDto()
        {
        }

        public PessoaDto(int id, string nome, Cpf cpf, string email, Telefone telefone)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            Email = email;
            Telefone = telefone;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public Cpf Cpf { get; set; }
        public string Email { get; set; }
        public Telefone Telefone { get; set; }
    }
}
