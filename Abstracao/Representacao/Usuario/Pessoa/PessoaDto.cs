using SharedKernel.ObjetosValor.Formatos;

namespace Abstracao.Representacao.Usuario.Pessoa
{
    public class PessoaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Cpf Cpf { get; set; }
        public string Email { get; set; }
        public Telefone Telefone { get; set; }
    }
}
