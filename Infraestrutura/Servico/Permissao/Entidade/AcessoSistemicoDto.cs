using System.Collections.Generic;
using infraestrutura.Servico.Permissao.Entidade;
using Infraestrutura.Repositorio.Permissao.Entidade;

namespace Infraestrutura.Servico.Permissao.Entidade
{
    public class AcessoSistemicoDto
    {
        public AcessoSistemicoDto()
        {
        }
        
        public AcessoSistemicoDto(int id, string descricao, bool ativo)
        {
            this.Id = id;
            this.Descricao = descricao;
            this.Ativo = ativo;
            Permissoes = new List<PermissaoDto>();
        }

        public AcessoSistemicoDto(AcessoSistemicoDpo acesso)
        {
            this.Id = acesso.Id;
            this.Descricao = acesso.Descricao;
            this.Ativo = acesso.Ativo;

            Permissoes = new List<PermissaoDto>();
            acesso.Permissoes.ForEach(p => Permissoes.Add(new PermissaoDto(p.Permissao, p.Descricao)));
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public List<PermissaoDto> Permissoes { get; set; }
        public bool Ativo { get; set; }
    }
}