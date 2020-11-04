using System;
using System.Collections.Generic;

namespace Abstracoes.Representacoes.Permissoes.Permissao
{
    public class AcessoSistemicoDto
    {
        public AcessoSistemicoDto(AcessoSistemicoDpo acesso)
        {
            Id = acesso.Id;
            Descricao = acesso.Descricao;
            DataCriacao = acesso.DataCriacao;
            Permissoes = new List<PermissaoDto>();
            
            foreach(var permissao in acesso.Permissoes)
                Permissoes.Add(new PermissaoDto(permissao));
        }

        public AcessoSistemicoDto(AcessoSistemicoDpo acesso, List<PermissaoDto> permissoes)
        {
            Id = acesso.Id;
            Descricao = acesso.Descricao;
            Permissoes = permissoes;
            DataCriacao = acesso.DataCriacao;
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public List<PermissaoDto> Permissoes { get; set; }
    }
}