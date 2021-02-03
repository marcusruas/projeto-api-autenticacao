using System.Collections.Generic;
using Infraestrutura.Repositorio.Permissao.Entidade;

namespace Infraestrutura.Servico.Permissao.Entidade
{
    public class AcessoSistemicoDpo
    {
        public AcessoSistemicoDpo()
        {
            Permissoes = new List<PermissaoDpo>();
        }

        public AcessoSistemicoDpo(int id, string descricao, bool ativo)
        {
            Id = id;
            Descricao = descricao;
            Ativo = ativo;
            Permissoes = new List<PermissaoDpo>();
        }
        
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public List<PermissaoDpo> Permissoes { get; set; }
    }
}