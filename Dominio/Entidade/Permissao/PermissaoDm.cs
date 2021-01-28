using System;
using System.Linq;
using System.Text.RegularExpressions;
using Entidade.Recurso;
using MandradePkgs.Mensagens;

namespace Dominio.Entidade.Permissao
{
    public class PermissaoDm
    {
        public PermissaoDm(string descricao)
        {
            Permissao = Guid.NewGuid();
            Descricao = descricao.ToUpper();
        }

        public PermissaoDm(int id, string descricao)
        {
            Id = id;
            Permissao = Guid.NewGuid();
            Descricao = descricao.ToUpper();
        }

        public int Id { get; set; }
        public Guid Permissao { get; set; }
        public string Descricao { get; set; }
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
            
            var matches = expressao.Matches(Descricao);
            if(!matches.Any())
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, Mensagens.PermissaoCaracteresInvalidos);
        }
    }
}