using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominio.Usuario
{
    public class UsuarioDom
    {
        public UsuarioDom(string nome, string usuario, string senha, string grupo, DateTime dataCriacao, bool ativo, string cpf, bool formatarCpf = false) {
            Nome = nome;
            Usuario = usuario;
            Senha = senha;
            Grupo = grupo;
            DataCriacao = dataCriacao;
            Ativo = ativo;
            Cpf = formatarCpf ? CpfFormat.FormatarCpf(cpf) : cpf;
        }

        public string Nome { get; }
        public string Usuario { get; }
        public string Senha { get; }
        public string Grupo { get; }
        public DateTime DataCriacao { get; }
        public bool Ativo { get; }
        public string Cpf { get; }

        public List<string> DadosInvalidos() {
            List<string> camposInvalidos = new List<string>();

            if (string.IsNullOrEmpty(Cpf))
                camposInvalidos.Add("Cpf");
            if (string.IsNullOrEmpty(Grupo)) 
                camposInvalidos.Add("Grupo");
            if (string.IsNullOrEmpty(Nome))
                camposInvalidos.Add("Nome");
            if (string.IsNullOrEmpty(Senha))
                camposInvalidos.Add("Senha");
            if (string.IsNullOrEmpty(Usuario))
                camposInvalidos.Add("Usuario");

            return camposInvalidos;
        }

         public bool SenhaValida() =>
                char.IsUpper(Senha[0]) && Senha.Length >= 8 && Senha.Count(c => char.IsNumber(c)) >= 3;

        public bool CpfValido() =>
            Cpf.Length == 14 && Cpf[3] == '.' && Cpf[7] == '.' && Cpf[12] == '-';

        public bool UsuarioValido() =>
            char.IsUpper(Usuario[0]);
    }
}
