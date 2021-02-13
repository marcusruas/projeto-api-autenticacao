using System;
using Infraestrutura.Repositorio.Entidade;
using Infraestrutura.Repositorio.Usuario.Entidade;

namespace Infraestrutura.Servico.Usuario.Entidade
{
    public class UsuarioDto
    {
        public UsuarioDto(
            int id, 
            string usuario, 
            DateTime dataCriacao, 
            DateTime dataCadastroSenha,
            int diasRenovacao,
            bool ativo, 
            GrupoDto grupo, 
            PessoaDto pessoa
        )
        {
            Id = id;
            Usuario = usuario;
            DataCriacao = dataCriacao;
            DataCadastroSenha = dataCadastroSenha;
            DiasRenovacao = diasRenovacao;
            Ativo = ativo;
            Grupo = grupo;
            Pessoa = pessoa;
        }

        public UsuarioDto(UsuarioDpo usuario, GrupoDpo grupo, PessoaDpo pessoa)
        {
            Id = usuario.Id;
            Usuario = usuario.Usuario;
            DataCriacao = usuario.DataCriacao;
            DataCadastroSenha = usuario.DataCadastroSenha;
            DiasRenovacao = usuario.DiasRenovacao;
            Ativo = usuario.Ativo;
            Grupo = new GrupoDto(grupo);
            Pessoa = new PessoaDto(pessoa);
        }

        public int Id { get; set; }
        public string Usuario { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataCadastroSenha { get; set; }
        public int DiasRenovacao { get; set; }
        public bool Ativo { get; set; }
        public GrupoDto Grupo { get; set; }
        public PessoaDto Pessoa { get; set; }
    }
}
