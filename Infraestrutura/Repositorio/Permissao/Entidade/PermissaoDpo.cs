using System;

namespace Infraestrutura.Repositorio.Permissao.Entidade
{
    public class PermissaoDpo
    {
        public PermissaoDpo()
        {
        }
        
        public PermissaoDpo(Guid permissao, string descricao)
        {
            Permissao = permissao;
            Descricao = descricao;
        }

        public PermissaoDpo(int id, Guid permissao, string descricao)
        {
            Id = id;
            Permissao = permissao;
            Descricao = descricao;
        }

        public int Id { get; set; }
        public Guid Permissao { get; set; }
        public string Descricao { get; set; }
    }
}