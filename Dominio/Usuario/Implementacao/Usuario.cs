using Dominio.Usuario.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dominio.Implementacao.Usuario
{
    public class Usuario : IUsuario
    {
        [Description("IDUSUARIO")]
        public int IdUsuario { get; set; }
        [Description("NOME")]
        public string Nome { get; set; }
        [Description("USUARIO")]
        public string Apelido { get; set; }
        [Description("SENHA")]
        public string Senha { get; set; }
        [Description("GRUPO")]
        public string Grupo { get; set; }
        [Description("DATACRIACAO")]
        public DateTime DataCriacao { get; set; }
        [Description("ATIVO")]
        public byte UsuarioAtivo { get; set; }
        [Description("CPF")]
        public string Cpf { get; set; }
    }
}
