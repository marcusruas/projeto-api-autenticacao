using Dominio.Entidade.Usuario;
using Dominio.ObjetoValor.Formatos;
using Infraestrutura.Repositorio.Usuario.Entidade;

namespace Infraestrutura.Servico.Usuario.Entidade
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

        public PessoaDto(PessoaDpo pessoa)
        {
            Cpf cpf = string.IsNullOrWhiteSpace(pessoa.Cpf) ?
                null : new Cpf(pessoa.Cpf.ToString());

            Telefone telefone =
                string.IsNullOrWhiteSpace(pessoa.Ddd) && string.IsNullOrWhiteSpace(pessoa.Numero) ?
                null : new Telefone(pessoa.Ddd, pessoa.Numero);

            Id = pessoa.Id;
            Nome = pessoa.Nome;
            Cpf = cpf;
            Email = pessoa.Email;
            Telefone = telefone;
        }

        public PessoaDto(PessoaDm pessoa)
        {
            Id = pessoa.Id;
            Nome = pessoa.Nome;
            Cpf = pessoa.Cpf;
            Email = pessoa.Email;
            Telefone = pessoa.Telefone;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public Cpf Cpf { get; set; }
        public string Email { get; set; }
        public Telefone Telefone { get; set; }
    }
}
