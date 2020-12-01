using System.ComponentModel.DataAnnotations;
using Dominio.Logica.Usuario;
using SharedKernel.ObjetosValor.Formatos;

namespace Abstracoes.Representacoes.Usuario.Pessoa
{
    public class PessoaAlteracaoDto
    {
        [Required(ErrorMessage = "ID da pessoa é obrigatório")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Nome da pessoa é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "CPF da pessoa é obrigatório")]
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string DddTelefone { get; set; }
        public string NumeroTelefone { get; set; }

        public bool possuiDadosAlteracao() {
            return !(string.IsNullOrWhiteSpace(Nome) && string.IsNullOrWhiteSpace(Email) &&
            string.IsNullOrWhiteSpace(DddTelefone) &&  string.IsNullOrWhiteSpace(NumeroTelefone) &&
            Cpf == null);
        }
    }
}
