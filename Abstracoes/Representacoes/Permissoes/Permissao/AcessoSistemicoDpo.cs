using System.Collections.Generic;

namespace Abstracoes.Representacoes.Permissoes.Permissao
{
    public class AcessoSistemicoDpo
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public List<int> Permissoes { get; set; }
    }
}