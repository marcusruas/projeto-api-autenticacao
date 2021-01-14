using System;

namespace infraestrutura.Servico.Permissao.Entidade
{
    public class PermissaoDto
    {
        public PermissaoDto(Guid permissao, string descricao)
        {
            Permissao = permissao;
            Descricao = descricao;
        }
        
        public Guid Permissao { get; set; }
        public string Descricao { get; set; }
    }
}