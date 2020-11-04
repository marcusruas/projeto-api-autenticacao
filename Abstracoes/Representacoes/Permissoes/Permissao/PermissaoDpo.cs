using System;
using System.ComponentModel.DataAnnotations;
using Logica.Permissoes;

namespace Abstracoes.Representacoes.Permissoes.Permissao
{
    public class PermissaoDpo
    {
        public PermissaoDpo()
        {
        }

        public PermissaoDpo(Guid permissao, string descricao, DateTime dataCriacao)
        {
            this.Permissao = permissao;
            this.Descricao = descricao;
            this.DataCriacao = dataCriacao;
        }

        public PermissaoDpo(PermissaoDom permissao)
        {
            this.Permissao = permissao.Permissao;
            this.Descricao = permissao.Descricao;
        }

        public PermissaoDpo(PermissaoAcessoDpo permissao)
        {
            this.Id = permissao.Id;
            this.Permissao = permissao.Permissao;
            this.Descricao = permissao.Descricao;
            this.DataCriacao = permissao.DataCriacao;
        }

        public int Id { get; set; }
        public Guid Permissao { get; set; }
        [StringLength(100)]
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}