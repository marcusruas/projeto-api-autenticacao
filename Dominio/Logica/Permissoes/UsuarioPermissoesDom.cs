using Dominio.Logica.Usuario;

namespace Logica.Permissoes
{
    public class UsuarioPermissoesDom
    {
        public UsuarioDom Grupo { get; set; }
        public AcessoSistemicoDom Acesso { get; set; }
        public bool Ativo { get; set; }
    }
}