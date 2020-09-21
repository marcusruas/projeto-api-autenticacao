using System;
using Dominio.Logica.Usuario;

namespace Abstracoes.Representacoes.Usuario.Usuario
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

        public UsuarioDpo(UsuarioDom usuario)
        {
            this.Id = usuario.Id;
            this.Usuario = usuario.Usuario;
            this.Senha = usuario.Senha.ValorCriptografado;
            this.DataCriacao = usuario.DataCriacao;
            this.Ativo = usuario.Ativo;
            this.idGrupo = usuario.Grupo.Id;
            this.idPessoa = usuario.Pessoa.Id;
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