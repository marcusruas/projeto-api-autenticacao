using System;

namespace Dominio.Entidade.Permissao
{
    public class PermissaoDm
    {
        public PermissaoDm(string descricao)
        {
            Permissao = Guid.NewGuid();
            Descricao = descricao;
        }

        public Guid Permissao { get; set; }
        public string Descricao { get; set; }
    }
}