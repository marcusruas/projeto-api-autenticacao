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
        public PermissaoDpo(Guid permissao, string descricao)
        {
            this.Permissao = permissao;
            this.Descricao = descricao;
        }
        public PermissaoDpo(PermissaoDom permissao)
        {
            this.Permissao = permissao.Permissao;
            this.Descricao = permissao.Descricao;
        }

        public int Id { get; set; }
        public Guid Permissao { get; set; }
        [StringLength(100)]
        public string Descricao { get; set; }
    }
}