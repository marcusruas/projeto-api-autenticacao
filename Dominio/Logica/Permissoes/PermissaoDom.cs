using System;

namespace Logica.Permissoes
{
    public class PermissaoDom
    {
        public PermissaoDom(string descricao)
        {
            Permissao = new Guid();
            Descricao = descricao.ToUpper();
        }
        public Guid Permissao { get; set; }
        public string Descricao { get; set; }
    }
}