using System.Collections.Generic;
using Infraestrutura.Repositorio.Permissao.Entidade;

namespace Infraestrutura.Servico.Permissao.Entidade
{
    public class AcessoSistemicoDpo
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public List<PermissaoDpo> Permissoes { get; set; }
    }
}