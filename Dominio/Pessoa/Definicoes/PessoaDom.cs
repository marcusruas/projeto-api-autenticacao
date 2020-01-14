﻿namespace Dominio.Pessoa.Definicoes
{
    public class PessoaDom
    {
        public PessoaDom(string nome, long cpf, string email, long telefone) {
            Nome = nome;
            Cpf = cpf;
            Email = email;
            Telefone = telefone;
        }

        public string Nome { get; }
        public long Cpf { get; set; }
        public string Email { get; set; }
        public long Telefone { get; set; }
    }
}