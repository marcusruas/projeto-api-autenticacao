using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Usuario.Interface
{
    public interface IUsuario
    {
        int IdUsuario { get; set; }
        string Nome { get; set; }
        string Apelido { get; set; }
        string Senha { get; set; }
        string Grupo { get; set; }
        DateTime DataCriacao { get; set; }
        byte UsuarioAtivo { get; set; }
        string Cpf { get; set; }
    }
}
