using System.Collections.Generic;

namespace Abstracoes.Representacoes.Permissoes.Permissao
{
    public class AcessoSistemicoDto
    {
        public string Desricao { get; set; }
        public List<PermissaoDto> Permissoes { get; set; }
    }
}