using System;
using System.ComponentModel.DataAnnotations;
using Logica.Permissoes;

namespace Abstracoes.Representacoes.Permissoes.Permissao
{
    public class PermissaoAcessoDpo
    {
        public PermissaoAcessoDpo()
        {
        }
        public PermissaoAcessoDpo(int acesso, int id, Guid permissao, string descricao, DateTime dataCriacao)
        {
            this.Acesso = acesso;
            this.Id = id;
            this.Permissao = permissao;
            this.Descricao = descricao;
            this.DataCriacao = dataCriacao;
        }

        public int Acesso { get; set; }
        public int Id { get; set; }
        public Guid Permissao { get; set; }
        [StringLength(100)]
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}