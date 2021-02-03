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
        
        public AcessoSistemicoDto(int id, bool ativo, string descricao)
        {
            Id = id;
            Ativo = ativo;
            Descricao = descricao;
            Permissoes = new List<PermissaoDto>();
        }

        public AcessoSistemicoDto(AcessoSistemicoDpo acesso)
        {
            Id = acesso.Id;
            Descricao = acesso.Descricao;
            Ativo = acesso.Ativo;
            Permissoes = new List<PermissaoDto>();
            acesso.Permissoes.ForEach(p => Permissoes.Add(new PermissaoDto(p)));
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public List<PermissaoDto> Permissoes { get; set; }
    }
}