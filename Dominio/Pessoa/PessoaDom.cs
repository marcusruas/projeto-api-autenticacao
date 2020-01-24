using Helpers;
using System.Linq;

namespace Dominio.Pessoa
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
        public long Cpf { get; }
        public string Email { get; }
        public long Telefone { get; } 

        public bool CpfValido => CpfHelper.ValidarCpf(Cpf.ToString());

        public bool NomeValido() {
            if (string.IsNullOrWhiteSpace(Nome))
                return false;

            var nomes = Nome.Split(' ').ToList();

            foreach (var nome in nomes)
                if (!char.IsUpper(nome[0]))
                    return false;

            return true;
        }
    }
}
