using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Abstracoes.Representacoes.Usuario.Pessoa
{
    public class PessoaDpo
    {
        public PessoaDpo()
        {
        }

        public PessoaDpo(int id, string nome, long cpf, string email, string ddd, string numero)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            Email = email;
            Ddd = ddd;
            Numero = numero;
        }

        [Description("ID_PESSOA")]
        public int Id { get; set; }
        [Description("NOME")]
        [StringLength(80)]
        public string Nome { get; set; }
        [Description("CPF")]
        public long Cpf { get; set; }
        [Description("EMAIL")]
        [StringLength(100)]
        public string Email { get; set; }
        [Description("DDD")]
        public string Ddd { get; set; }
        [Description("NUMERO")]
        public string Numero { get; set; }
    }
}
