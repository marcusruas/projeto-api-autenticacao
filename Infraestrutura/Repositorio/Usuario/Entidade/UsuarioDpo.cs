using System;
using Dominio.ObjetoValor.Formatos;

namespace Infraestrutura.Repositorio.Entidade
{
    public class UsuarioDpo
    {

        public UsuarioDpo(int id, string usuario, string senha, DateTime dataCriacao, bool ativo, int idGrupo, int idPessoa)
        {
            this.Id = id;
            this.Usuario = usuario;
            this.Senha = senha;
            this.DataCriacao = dataCriacao;
            this.Ativo = ativo;
            this.idGrupo = idGrupo;
            this.idPessoa = idPessoa;
        }

        public UsuarioDpo(int id, string usuario, Senha senha, DateTime dataCriacao, bool ativo, int idGrupo, int idPessoa)
        {
            this.Id = id;
            this.Usuario = usuario;
            this.Senha = senha.ValorCriptografado;
            this.DataCriacao = dataCriacao;
            this.Ativo = ativo;
            this.idGrupo = idGrupo;
            this.idPessoa = idPessoa;
        }

        public int Id { get; }
        public string Usuario { get; }
        public string Senha { get; }
        public DateTime DataCriacao { get; }
        public bool Ativo { get; }
        public int idGrupo { get; }
        public int idPessoa { get; }
    }
}