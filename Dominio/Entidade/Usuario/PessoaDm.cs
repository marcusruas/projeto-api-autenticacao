using MandradePkgs.Mensagens;
using Dominio.ObjetoValor.Formatos;
using System;
using System.Linq;
using Entidade.Recurso;

namespace Dominio.Entidade.Usuario
{
    public class PessoaDm
    {
        public PessoaDm(int id, string cpf)
        {
            Id = id;
            Cpf = new Cpf(cpf);
        }

        public PessoaDm(int id, string nome, Cpf cpf, string email, Telefone telefone)
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
            if (this._mensagens == null)
                this.mensagens = mensagens;
            else
                throw new ArgumentException(Mensagens.MensageriaErroSobrescrita);
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
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, Mensagens.NomeObrigatorio);
            else
            {
                var nomes = Nome.Split(' ').ToList();
                foreach (var nome in nomes)
                {
                    if (!char.IsUpper(nome[0]))
                    {
                        _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, Mensagens.PessoaNomeMaiusculo);
                        return;
                    }
                }
                        
            }
        }

        public void ValidarCpf()
        {
            if (Cpf != null && !string.IsNullOrWhiteSpace(Cpf.ValorNumerico))
                if (!Cpf.CpfValido())
                    _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, Mensagens.PessoaCPFInvalido);
        }

        public void ValidarTelefone()
        {
            if (Telefone == null || (string.IsNullOrEmpty(Telefone.Ddd) && string.IsNullOrEmpty(Telefone.Numero)))
                return;

            if (string.IsNullOrEmpty(Telefone.Ddd))
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, Mensagens.PessoaTelefoneInvalido);
            if (string.IsNullOrEmpty(Telefone.Numero) || Telefone.Numero.Length < 8 || Telefone.Numero.Length > 9)
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, Mensagens.PessoaNumeroInvalido);
        }

        public void ValidarDadosContato()
        {
            if (
                string.IsNullOrEmpty(Email) &&
                Telefone == null
            )
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, Mensagens.PessoaFormaContato);
        }
    }
}
