using Dominio.Grupo;
using Dominio.Pessoa;
using System;
using System.Linq;

namespace Dominio.Usuario.Definicoes
{
    public class UsuarioDom
    {
        public UsuarioDom(string usuario, string senha, DateTime dataCriacao, bool ativo, GrupoDom grupo, PessoaDom pessoa) {
            Usuario = usuario;
            Senha = senha;
            DataCriacao = dataCriacao;
            Ativo = ativo;
            Grupo = grupo;
            Pessoa = pessoa;
        }

        public UsuarioDom(string usuario, string senha, GrupoDom grupo, PessoaDom pessoa) {
            Usuario = usuario;
            Senha = senha;
            Grupo = grupo;
            Pessoa = pessoa;
            DataCriacao = DateTime.Now;
            Ativo = true;
        }

        public string Usuario { get; }
        public string Senha { get; }
        public DateTime DataCriacao { get; }
        public bool Ativo { get; }
        public GrupoDom Grupo { get; }
        public PessoaDom Pessoa { get; }

        public bool UsuarioValido() =>
            char.IsUpper(Usuario[0]) && Usuario.Length > 4;

        public bool SenhaValida() =>
            char.IsUpper(Senha[0]) && Senha.Length >= 8 && Senha.Count(c => char.IsNumber(c)) >= 3;
    }
}
