using MandradePkgs.Mensagens;
using System;
using System.Linq;

namespace Dominio.Logica.Usuario
{
    public class GrupoDom
    {

        public GrupoDom(int id, string nome, string descricao, int pai)
        {
            this.Id = id;
            this.Nome = nome;
            this.Descricao = descricao;
            this.Pai = pai;
        }

        public GrupoDom(int id, string nome, string descricao)
        {
            this.Id = id;
            this.Nome = nome;
            this.Descricao = descricao;
        }
        public int Id { get; }
        public string Nome { get; }
        public string Descricao { get; }
        public int Pai { get; }
        private IMensagensApi mensagens { get; set; }
        public IMensagensApi _mensagens
        {
            get { return mensagens; }
            private set { DefinirMensagens(value); }
        }

        public const int LimiteCaracteresNome = 80;
        public const int LimiteCaracteresDescricao = 200;

        public void ValidarDados()
        {
            ValidarNome();
            ValidarDescricao();
        }

        public void DefinirMensagens(IMensagensApi mensagens)
        {
            if (this._mensagens != null)
                this.mensagens = mensagens;
            else
                throw new ArgumentException("Não é possível sobrescrever a mensageria do objeto");
        }

        public void ValidarNome()
        {
            if (string.IsNullOrEmpty(Nome))
            {
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, "Nome é obrigatório");
                return;
            }

            if (Nome.Length <= 5)
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao,
                                             "Nome do grupo deve ter mais de 5 caractéres");
            if (Nome.Length > LimiteCaracteresNome)
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao,
                                             "Nome do grupo deve ter menos de 80 caractéres");
            if (Nome.Any(x => char.IsNumber(x)))
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao,
                                             "Nome do grupo não pode conter números");
        }

        public void ValidarDescricao()
        {
            if (!string.IsNullOrEmpty(Descricao))
            {
                if (Descricao.Length <= 15)
                    _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao,
                                                 "Descrição do grupo deve conter mais de 15 caractéres");
                if (Descricao.Length > LimiteCaracteresDescricao)
                    _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao,
                                                $"Descrição do grupo deve conter menos de {LimiteCaracteresDescricao} caractéres");
            }
        }
    }
}