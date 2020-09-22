using MandradePkgs.Mensagens;
using SharedKernel.ObjetosValor.Formatos;
using System;
using System.Linq;

namespace Dominio.Logica.Usuario
{
    public class PessoaDom
    {
        public PessoaDom(int id, string cpf)
        {
            Id = id;
            Cpf = new Cpf(cpf);
        }

        public PessoaDom(int id, string nome, Cpf cpf, string email, Telefone telefone)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            Email = email;
            Telefone = telefone;
        }

        public int Id { get; }
        public string Nome { get; }
        public Cpf Cpf { get; }
        public string Email { get; }
        public Telefone Telefone { get; }
        private IMensagensApi mensagens { get; set; }
        public IMensagensApi _mensagens
        {
            get { return mensagens; }
            private set { DefinirMensagens(value); }
        }

        public void DefinirMensagens(IMensagensApi mensagens)
        {
            if (this._mensagens != null)
                this.mensagens = mensagens;
            else
                throw new ArgumentException("Não é possível sobrescrever a mensageria do objeto");
        }

        public void ValidarDados()
        {
            ValidarNome();
            ValidarCpf();
            ValidarTelefone();
            ValidarDadosContato();
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

        public void ValidarCpf()
        {
            if (Cpf != null && !string.IsNullOrWhiteSpace(Cpf.ValorNumerico))
                if (!Cpf.CpfValido())
                    _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, "CPF inválido, verifique os dados.");
        }

        public void ValidarTelefone()
        {
            if (Telefone == null)
                return;

            if (string.IsNullOrEmpty(Telefone.Ddd) && string.IsNullOrEmpty(Telefone.Numero))
                return;

            if (string.IsNullOrEmpty(Telefone.Ddd))
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, "Telefone da pessoa deve conter DDD.");
            if (string.IsNullOrEmpty(Telefone.Numero) || Telefone.Numero.Length < 8 || Telefone.Numero.Length > 9)
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, "Número de telefone inválido.");
        }

        public void ValidarDadosContato()
        {
            if (
                string.IsNullOrEmpty(Email) &&
                Telefone == null
            )
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, "Ao menos uma forma de contato deve ser fornecido da pessoa.");
        }
    }
}
