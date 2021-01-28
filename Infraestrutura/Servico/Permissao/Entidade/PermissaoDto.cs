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
            Descricao = permissao.Descricao;
        }
        
        public int Id { get; set; }
        public Guid Permissao { get; set; }
        public string Descricao { get; set; }
    }
}