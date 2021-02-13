using System;
namespace Infraestrutura.Repositorio.Entidade
{
    public class UsuarioDpo
    {

        public UsuarioDpo(
            int id, 
            string usuario, 
            string senha, 
            DateTime dataCriacao, 
            DateTime dataCadastroSenha,
            int diasRenovacao, 
            bool ativo, 
            int idGrupo, 
            int idPessoa
        )
        {
            Id = id;
            Usuario = usuario;
            Senha = senha;
            DataCriacao = dataCriacao;
            DataCadastroSenha = dataCadastroSenha;
            DiasRenovacao = diasRenovacao;
            Ativo = ativo;
            IdGrupo = idGrupo;
            IdPessoa = idPessoa;
        }

        public int Id { get; }
        public string Usuario { get; }
        public string Senha { get; }
        public DateTime DataCriacao { get; }
        public DateTime DataCadastroSenha { get; set; }
        public int DiasRenovacao { get; set; }
        public bool Ativo { get; }
        public int IdGrupo { get; }
        public int IdPessoa { get; }
    }
}