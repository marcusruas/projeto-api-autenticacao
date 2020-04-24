using SharedKernel.ObjetosValor.Formatos;

namespace Abstracoes.Representacoes.Usuario.Pessoa
{
    public class FiltroBuscaPessoasDto
    {
        public FiltroBuscaPessoasDto(string nome, string cpf)
        {
            if (!string.IsNullOrWhiteSpace(nome))
                Nome = nome;
            if (!string.IsNullOrWhiteSpace(cpf))
                Cpf = new Cpf(cpf);
        }

        public string Nome { get; set; }
        public Cpf Cpf { get; set; }

        public bool PossuiNome() => !string.IsNullOrWhiteSpace(Nome);
        public bool PossuiCpf() => Cpf != null;
    }
}