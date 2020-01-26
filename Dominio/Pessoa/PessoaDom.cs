using Helpers;
using MandradePkgs.Mensagens;
using System.Linq;

namespace Dominio.Pessoa
{
    public class PessoaDom
    {
        public PessoaDom(string nome, long cpf, string email, long telefone, IMensagensApi mensagens) {
            Nome = nome;
            Cpf = cpf;
            Email = email;
            Telefone = telefone;
            _mensagens = mensagens;
        }

        public PessoaDom(long cpf, IMensagensApi mensagens) {
            Cpf = cpf;
            _mensagens = mensagens;
        }

        public string Nome { get; }
        public long Cpf { get; }
        public string Email { get; }
        public long Telefone { get; } 
        private IMensagensApi _mensagens { get; }

        public void ValidarCpf() {
            if (!CpfHelper.ValidarCpf(Cpf.ToString()))
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, "CPF inválido, verifique os dados.");
        }

        public void ValidarNome() {
            if (string.IsNullOrWhiteSpace(Nome))
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, "Nome é obrigatório.");
            else {
                var nomes = Nome.Split(' ').ToList();
                var mensagemErro = "Nome e sobrenomes dos usuários devem iniciar com letra maiúscula";
                foreach (var nome in nomes)
                    if (!char.IsUpper(nome[0]) && !_mensagens.Mensagens.Any(m => m.Texto == mensagemErro))
                        _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, mensagemErro);
            }
            
        }
    }
}
