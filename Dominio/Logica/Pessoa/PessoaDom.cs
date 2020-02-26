using Dominio.ObjetosValor;
using MandradePkgs.Mensagens;
using System.Linq;

namespace Dominio.Logica.Pessoa
{
    public class PessoaDom
    {
        public PessoaDom(string nome, string cpfFormatado, string email, string telefone, IMensagensApi mensagens)
        {
            Nome = nome;
            Cpf = new Cpf(cpfFormatado);
            Email = email;
            Telefone = new Telefone(telefone);
            _mensagens = mensagens;
        }

        public PessoaDom(string nome, string cpf, string email, long telefone, IMensagensApi mensagens)
        {
            Nome = nome;
            Cpf = new Cpf(cpf);
            Email = email;
            Telefone = new Telefone(telefone);
            _mensagens = mensagens;
        }

        public PessoaDom(string cpf, IMensagensApi mensagens)
        {
            Cpf = new Cpf(cpf);
            _mensagens = mensagens;
        }

        public string Nome { get; }
        public Cpf Cpf { get; }
        public string Email { get; }
        public Telefone Telefone { get; }
        private IMensagensApi _mensagens { get; }

        public void ValidarCpf()
        {
            if (!Cpf.CpfValido())
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, "CPF inválido, verifique os dados.");
        }

        public void ValidarNome()
        {
            if (string.IsNullOrWhiteSpace(Nome))
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, "Nome é obrigatório.");
            else
            {
                var nomes = Nome.Split(' ').ToList();
                var mensagemErro = "Nome e sobrenomes dos usuários devem iniciar com letra maiúscula";
                foreach (var nome in nomes)
                    if (!char.IsUpper(nome[0]) && !_mensagens.Mensagens.Any(m => m.Texto == mensagemErro))
                        _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, mensagemErro);
            }

        }
    }
}
