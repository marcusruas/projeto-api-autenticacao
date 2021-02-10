using System;

namespace Infraestrutura.Repositorio.Permissao.Entidade
{
    public class PermissaoDpo
    {
        public PermissaoDpo()
        {
        }

        public PermissaoDpo(Guid permissao, string nome, string descricao, bool ativo)
        {
            Permissao = permissao;
            Nome = nome;
            Descricao = descricao;
            Ativo = ativo;
        }

        public PermissaoDpo(int id, Guid permissao, string nome, string descricao, bool ativo)
        {
            Id = id;
            Permissao = permissao;
            Nome = nome;
            Descricao = descricao;
            Ativo = ativo;
        }

        public int Id { get; set; }
        public Guid Permissao { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
    }
}