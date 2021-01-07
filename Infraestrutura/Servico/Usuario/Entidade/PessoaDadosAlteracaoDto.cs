using System.ComponentModel.DataAnnotations;

namespace Infraestrutura.Servico.Usuario.Entidade
{
    public class PessoaDadosAlteracaoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string DddTelefone { get; set; }
        public string NumeroTelefone { get; set; }
    }
}
