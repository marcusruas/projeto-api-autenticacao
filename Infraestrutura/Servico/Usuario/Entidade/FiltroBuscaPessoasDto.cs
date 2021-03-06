using Dominio.ObjetoValor.Formatos;

namespace Infraestrutura.Servico.Usuario.Entidade
{
    public class FiltroBuscaPessoasDto
    {
        public string nome { get; set; }
        private Cpf _cpf { get; set; }
        public string cpf {
            get { return _cpf?.ValorNumerico; }
            set { _cpf = new Cpf(value); }
        }

        public bool PossuiNome() => !string.IsNullOrWhiteSpace(nome);
        public bool PossuiCpf() => cpf != null;
    }
}