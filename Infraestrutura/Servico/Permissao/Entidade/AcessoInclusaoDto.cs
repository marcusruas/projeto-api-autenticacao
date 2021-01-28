using System;
using System.Collections.Generic;

namespace Servico.Permissao.Entidade
{
    public class AcessoInclusaoDto
    {
        public string Descricao { get; set; }
        public List<Guid> Permissoes { get; set; }
    }
}