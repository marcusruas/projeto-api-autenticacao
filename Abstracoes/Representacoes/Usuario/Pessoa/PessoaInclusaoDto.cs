using System.ComponentModel.DataAnnotations;

namespace Abstracoes.Representacoes.Usuario.Pessoa
{
    public class PessoaInclusaoDto
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Ddd { get; set; }
        public string Numero { get; set; }
    }
}