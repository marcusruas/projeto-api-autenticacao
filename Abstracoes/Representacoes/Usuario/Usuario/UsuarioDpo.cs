using System;

namespace Abstracoes.Representacoes.Usuario.Usuario
{
    public class UsuarioDpo
    {
        public int Id { get; }
        public string Usuario { get; }
        public string Senha { get; }
        public DateTime DataCriacao { get; }
        public bool Ativo { get; }
        public int idGrupo { get; }
        public int idPessoa { get; }
    }
}