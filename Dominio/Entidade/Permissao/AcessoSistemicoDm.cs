using System.Collections.Generic;

namespace Dominio.Entidade.Permissao
{
    public class AcessoSistemicoDm
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public List<PermissaoDm> Permissoes { get; set; }
        public bool Ativo { get; set; }
    }
}