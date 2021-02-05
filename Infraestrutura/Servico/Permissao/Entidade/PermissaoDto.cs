using System;
using Infraestrutura.Repositorio.Permissao.Entidade;

namespace Infraestrutura.Servico.Permissao.Entidade
{
    public class PermissaoDto
    {
        public PermissaoDto(Guid permissao, string descricao)
        {
            Permissao = permissao;
            Descricao = descricao;
        }

        public PermissaoDto(PermissaoDpo permissao)
        {
            Id = permissao.Id;
            Permissao = permissao.Permissao;
            Nome = permissao.Nome;
            Descricao = permissao.Descricao;
            Ativo = permissao.Ativo;
        }
        
        public int Id { get; set; }
        public Guid Permissao { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
    }
}