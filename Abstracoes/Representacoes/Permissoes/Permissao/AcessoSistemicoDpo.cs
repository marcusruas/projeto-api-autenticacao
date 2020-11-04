using System;
using System.Collections.Generic;

namespace Abstracoes.Representacoes.Permissoes.Permissao
{
    public class AcessoSistemicoDpo
    {
        public AcessoSistemicoDpo()
        {
            Permissoes = new List<PermissaoDpo>();
        }
        
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public List<PermissaoDpo> Permissoes { get; set; }
    }
}