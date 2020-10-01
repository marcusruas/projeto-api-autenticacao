using Dominio.Logica.Usuario;

namespace Logica.Permissoes
{
    public class GrupoPermissoesDom
    {
        public GrupoDom Grupo { get; set; }
        public AcessoSistemicoDom Acesso { get; set; }
        public bool Ativo { get; set; }
    }
}