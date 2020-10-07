using System.Collections.Generic;

namespace Abstracoes.Representacoes.Permissoes.Permissao
{
    public class AcessoSistemicoDto
    {
        public AcessoSistemicoDto(AcessoSistemicoDpo acesso)
        {
            Id = acesso.Id;
            Descricao = acesso.Descricao;
        }

        public AcessoSistemicoDto(AcessoSistemicoDpo acesso, List<PermissaoDto> permissoes)
        {
            Id = acesso.Id;
            Descricao = acesso.Descricao;
            Permissoes = permissoes;
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public List<PermissaoDto> Permissoes { get; set; }
    }
}