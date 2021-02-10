using System;
using System.Linq;
using System.Text.RegularExpressions;
using Entidade.Recurso;
using MandradePkgs.Mensagens;

namespace Dominio.Entidade.Permissao
{
    public class PermissaoDm
    {
        public PermissaoDm(string nome, string descricao)
        {
            Permissao = Guid.NewGuid();
            Nome = nome.ToUpper();
            Descricao = descricao;
            Ativo = true;
        }

        public PermissaoDm(int id, Guid permissao, string nome, string descricao, bool ativo)
        {
            Id = id;
            Permissao = permissao;
            Nome = nome.ToUpper();
            Descricao = descricao;
            Ativo = ativo;
        }

        public int Id { get; set; }
        public Guid Permissao { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
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

        public void PossuiCaracteresInvalidos() {
            var expressao = new Regex(@"^[a-z A-Z]*$");
            
            var matches = expressao.Matches(Nome);
            if(!matches.Any())
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, Mensagens.PermissaoCaracteresInvalidos);
        }
    }
}