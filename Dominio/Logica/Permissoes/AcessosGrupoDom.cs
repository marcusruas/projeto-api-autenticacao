using System;
using Dominio.Logica.Usuario;

namespace Logica.Permissoes
{
    public class AcessoGrupoDom
    {
        public int Id { get; set; }
        public GrupoDom Grupo { get; set; }
        public PermissaoDom Permissao { get; set; }
        public string Justificativa { get; set; }
        public UsuarioDom UsuarioResponsavel { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}