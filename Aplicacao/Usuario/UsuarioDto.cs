using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacao.Usuario
{
    public class UsuarioDto
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public string Senha { get; set; }
        public string Grupo { get; set; }
        public DateTime DataCriacao { get; set; }
        public byte UsuarioAtivo { get; set; }
        public string Cpf { get; set; }
    }
}
