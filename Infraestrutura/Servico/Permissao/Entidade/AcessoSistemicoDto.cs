using System.Collections.Generic;
using Infraestrutura.Servico.Permissao.Entidade;
using Infraestrutura.Repositorio.Permissao.Entidade;

namespace Infraestrutura.Servico.Permissao.Entidade
{
    public class AcessoSistemicoDto
    {
        public AcessoSistemicoDto()
        {
        }
        
        public AcessoSistemicoDto(int id, string descricao)
        {
            this.Id = id;
            this.Descricao = descricao;
            Permissoes = new List<PermissaoDto>();
        }

        public AcessoSistemicoDto(AcessoSistemicoDpo acesso)
        {
            this.Id = acesso.Id;
            this.Descricao = acesso.Descricao;

            Permissoes = new List<PermissaoDto>();
            acesso.Permissoes.ForEach(p => Permissoes.Add(new PermissaoDto(p.Permissao, p.Descricao)));
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public List<PermissaoDto> Permissoes { get; set; }
    }
}