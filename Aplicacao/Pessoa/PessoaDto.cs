using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Aplicacao.Pessoa
{
    public class PessoaDto
    {
        public int Id { get; }
        public string Nome { get; }
        public long Cpf { get; set; }
        public string Email { get; set; }
        public long Telefone { get; set; }
    }
}
