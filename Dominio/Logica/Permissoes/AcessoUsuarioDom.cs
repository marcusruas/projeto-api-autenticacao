using System;
using Dominio.Logica.Usuario;

namespace Logica.Permissoes
{
    public class AcessoUsuarioDom
    {
        public int Id { get; set; }
        public UsuarioDom usuario { get; set; }
        public PermissaoDom Permissao { get; set; }
        public string Justificativa { get; set; }
        public UsuarioDom UsuarioResponsavel { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}