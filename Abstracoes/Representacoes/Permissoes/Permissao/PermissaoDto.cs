using System;
using System.ComponentModel.DataAnnotations;
using Logica.Permissoes;

namespace Abstracoes.Representacoes.Permissoes.Permissao
{
    public class PermissaoDto
    {
        public PermissaoDto()
        {
        }
        public PermissaoDto(Guid permissao, string descricao)
        {
            this.Permissao = permissao;
            this.Descricao = descricao;
        }
        public PermissaoDto(PermissaoDom permissao)
        {
            this.Permissao = permissao.Permissao;
            this.Descricao = permissao.Descricao;
        }

        public PermissaoDto(PermissaoDpo permissao)
        {
            this.Id = permissao.Id;
            this.Permissao = permissao.Permissao;
            this.Descricao = permissao.Descricao;
            this.DataCriacao = permissao.DataCriacao;
        }

        public int Id { get; set; }
        public Guid Permissao { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}