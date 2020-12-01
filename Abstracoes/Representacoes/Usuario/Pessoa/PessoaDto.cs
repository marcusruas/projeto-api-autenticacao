using System.ComponentModel.DataAnnotations;
using Dominio.Logica.Usuario;
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

        public PessoaDto(PessoaDom pessoa)
        {
            Id = pessoa.Id;
            Nome = pessoa.Nome;
            Cpf = pessoa.Cpf;
            Email = pessoa.Email;
            Telefone = pessoa.Telefone;
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Nome da pessoa é obrigatório")]
        public string Nome { get; set; }
        public Cpf Cpf { get; set; }
        public string Email { get; set; }
        public Telefone Telefone { get; set; }
    }
}
