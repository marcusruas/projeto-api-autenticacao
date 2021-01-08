using MandradePkgs.Mensagens;
using System;
using System.Linq;
using Entidade.Recurso;

namespace Dominio.Entidade.Usuario
{
    public class GrupoDm
    {

        public GrupoDm(int id, string nome, string descricao, int pai)
        {
            this.Id = id;
            this.Nome = nome;
            this.Descricao = descricao;
            this.Pai = pai;
        }

        public GrupoDm(int id, string nome, string descricao)
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
            if (this._mensagens == null)
                this.mensagens = mensagens;
            else
                throw new ArgumentException(Mensagens.MensageriaErroSobrescrita);
        }

        public void ValidarNome()
        {
            if (string.IsNullOrEmpty(Nome))
            {
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, Mensagens.NomeObrigatorio);
                return;
            }

            if (Nome.Length <= 5)
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, Mensagens.GrupoNomeMinimo.Replace('N', '5'));
            if (Nome.Length > LimiteCaracteresNome)
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, Mensagens.GrupoNomeMaximo.Replace("N", LimiteCaracteresNome.ToString()));
            if (Nome.Any(x => char.IsNumber(x)))
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, Mensagens.GrupoNomeNumeros);
        }

        public void ValidarDescricao()
        {
            if (!string.IsNullOrEmpty(Descricao))
            {
                if (Descricao.Length <= 15)
                    _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, Mensagens.GrupoDescricaoMinimo.Replace("N", "20"));
                if (Descricao.Length > LimiteCaracteresDescricao)
                    _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, Mensagens.GrupoDescricaoMinimo.Replace("N", LimiteCaracteresDescricao.ToString()));
            }
        }
    }
}