using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Dominio.ObjetoValor.Formatos;

namespace Infraestrutura.Repositorio.Usuario.Entidade
{
    public class PessoaDpo
    {
        public PessoaDpo()
        {
        }

        public PessoaDpo(int id, string nome, string cpf, string email, string ddd, string numero)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            Email = email;
            Ddd = ddd;
            Numero = numero;
        }

        public PessoaDpo(int id, string nome, Cpf cpf, string email, Telefone telefone)
        {
            bool possuiTelefone = telefone != null;

            Id = id;
            Nome = nome;
            Cpf = cpf?.ValorNumerico;
            Email = email;
            Ddd = possuiTelefone ? telefone.Ddd : null;
            Numero = possuiTelefone ? telefone.Numero : null;
        }
        
        [Description("ID_PESSOA")]
        public int Id { get; set; }
        [Description("NOME")]
        [StringLength(80)]
        public string Nome { get; set; }
        [Description("CPF")]
        public string Cpf { get; set; }
        [Description("EMAIL")]
        [StringLength(100)]
        public string Email { get; set; }
        [Description("DDD")]
        public string Ddd { get; set; }
        [Description("NUMERO")]
        public string Numero { get; set; }
    }
}
