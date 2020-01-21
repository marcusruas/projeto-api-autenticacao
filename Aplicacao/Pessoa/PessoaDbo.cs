using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Aplicacao.Pessoa
{
    public class PessoaDbo
    {
        [Description("ID_PESSOA")]
        public int Id { get; set; }
        [StringLength(80)]
        public string Nome { get; set; }
        public long Cpf { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        public long Telefone { get; set; }
    }
}
