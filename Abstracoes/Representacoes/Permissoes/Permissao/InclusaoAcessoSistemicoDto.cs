using System.Collections.Generic;

namespace Abstracoes.Representacoes.Permissoes.Permissao
{
    public class InclusaoAcessoSistemicoDto
    {
        public string Desricao { get; set; }
        public List<int> Permissoes { get; set; }
    }
}