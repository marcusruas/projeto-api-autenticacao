using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Representacao.Pessoa
{
    public class PessoaDpo
    {
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
        [Description("TELEFONE")]
        public long Telefone { get; set; }
    }
}
