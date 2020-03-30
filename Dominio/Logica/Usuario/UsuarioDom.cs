using System;
using System.Linq;
using SharedKernel.ObjetosValor.Formatos;
using MandradePkgs.Mensagens;

namespace Dominio.Logica.Usuario
{
    public class UsuarioDom
    {
        public UsuarioDom(int id, string usuario, string senha, DateTime dataCriacao, bool ativo, GrupoDom grupo, PessoaDom pessoa, IMensagensApi mensagens)
        {
            Id = id;
            Usuario = usuario;
            Senha = new Senha(senha);
            DataCriacao = dataCriacao;
            Ativo = ativo;
            Grupo = grupo;
            Pessoa = pessoa;
            _mensagens = mensagens;
        }

        public UsuarioDom(int id, string usuario, string senha, GrupoDom grupo, PessoaDom pessoa, IMensagensApi mensagens)
        {
            Id = id;
            Usuario = usuario;
            Senha = new Senha(senha);
            Grupo = grupo;
            Pessoa = pessoa;
            DataCriacao = DateTime.Now;
            Ativo = true;
            _mensagens = mensagens;
        }

        public int Id { get; }
        public string Usuario { get; }
        public Senha Senha { get; }
        public DateTime DataCriacao { get; }
        public bool Ativo { get; }
        public GrupoDom Grupo { get; }
        public PessoaDom Pessoa { get; }
        private IMensagensApi _mensagens { get; }

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
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, "Usuário é obrigatório");
                return;
            }

            if (!char.IsUpper(Usuario[0]))
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, "Usuário deve iniciar com letra maiúscula");

            if (Usuario.Length < 5)
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, "Usuário deve conter mais de 5 caractéres");
        }

        public void ValidarSenha()
        {
            if (Senha.SenhaNula())
            {
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, "Senha do usuário é obrigatória");
                return;
            }

            if (!Senha.PossuiCaracteresNecessarios())
                _mensagens.AdicionarMensagem(
                    TipoMensagem.FalhaValidacao,
                    "Senha do usuário deve conter ao menos uma letra maiúscula, um número e um caractere especial"
                );

            if (!Senha.PossuiMinimoCaracteres())
                _mensagens.AdicionarMensagem(
                    TipoMensagem.FalhaValidacao,
                    $"Senha deve conter mais de {Senha.MinimoCaracteresNecessarios} caractéres"
                );
        }
    }
}
