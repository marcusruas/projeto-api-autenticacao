using System;

namespace Infraestrutura.Repositorio.Permissao.Entidade
{
    public class PermissaoDpo
    {
        public PermissaoDpo(Guid permissao, string descricao)
        {
            Permissao = permissao;
            Descricao = descricao;
        }

        public Guid Permissao { get; set; }
        public string Descricao { get; set; }
    }
}