using SharedKernel.ObjetosValor.Formatos;

namespace Abstracoes.Representacoes.Usuario.Pessoa
{
    public class FiltroBuscaPessoasDto
    {
        public string Nome { get; set; }
        private Cpf cpf { get; set; }
        public string Cpf {
            get { return cpf?.ValorNumerico; }
            set { cpf = new Cpf(value); }
        }

        public bool PossuiNome() => !string.IsNullOrWhiteSpace(Nome);
        public bool PossuiCpf() => Cpf != null;
    }
}