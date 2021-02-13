using System;
using System.Linq;
using Dominio.ObjetoValor.Formatos;
using Entidade.Recurso;
using MandradePkgs.Mensagens;

namespace Dominio.Entidade.Usuario
{
    public class UsuarioDm
    {
        public UsuarioDm(
            int id, 
            string usuario, 
            string senha, 
            DateTime dataCriacao, 
            bool ativo, 
            DateTime dataCadastroSenha,
            int diasRenovacao, 
            GrupoDm grupo, 
            PessoaDm pessoa
        )
        {
            Id = id;
            Usuario = usuario;
            Senha = new Senha(senha);
            DataCriacao = dataCriacao;
            DataCadastroSenha = dataCadastroSenha;
            DiasRenovacao = diasRenovacao;
            Grupo = grupo;
            Pessoa = pessoa;

            Ativo = ativo && !PossuiSenhaExpirada();
        }

        public UsuarioDm(
            int id, 
            string usuario, 
            string senha, 
            int diasRenovacao,
            GrupoDm grupo, 
            PessoaDm pessoa
        )
        {
            Id = id;
            Usuario = usuario;
            Senha = new Senha(senha);
            Grupo = grupo;
            Pessoa = pessoa;
            DataCriacao = DateTime.Now;
            DataCadastroSenha = new DateTime(DataCriacao.Year, DataCriacao.Month, DataCriacao.Day);
            DiasRenovacao = diasRenovacao;
            Ativo = true;
        }

        public int Id { get; }
        public string Usuario { get; }
        public Senha Senha { get; }
        public DateTime DataCriacao { get; }
        public DateTime DataCadastroSenha { get; set; }
        public int DiasRenovacao { get; set; }
        public bool Ativo { get; }
        public GrupoDm Grupo { get; }
        public PessoaDm Pessoa { get; }
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
            ValidarUsuario();
            ValidarSenha();
            Grupo.ValidarDados();
            Pessoa.ValidarDados();
        }

        public void ValidarUsuario()
        {
            if (string.IsNullOrWhiteSpace(Usuario))
            {
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, Mensagens.UsuarioObrigatorio);
                return;
            }

            if (!char.IsUpper(Usuario[0]))
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, Mensagens.UsuarioLetraMaiuscula);

            if (Usuario.Length < 5)
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, Mensagens.UsuarioMinimo.Replace("N", "5"));
        }

        public void ValidarSenha()
        {
            if (Senha.SenhaNula())
            {
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, Mensagens.UsuarioSenhaObrigatoria);
                return;
            }

            if (!Senha.PossuiCaracteresNecessarios())
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, Mensagens.UsuarioSenhaEspecificacoes);

            if (!Senha.PossuiMinimoCaracteres())
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, Mensagens.UsuarioSenhaMinima.Replace("N", Senha.MinimoCaracteresNecessarios.ToString()));
        }

        public bool PossuiSenhaExpirada() {
            DateTime dataAtual = DateTime.Now;
            bool senhaExpirada = DataCadastroSenha.AddDays(DiasRenovacao) < dataAtual;
            return senhaExpirada;
        }
    }
}
