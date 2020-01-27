using Dominio.Grupo;
using MandradePkgs.Mensagens;
using MandradePkgs.Mensagens.Estrutura.Implementacao;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AutenticacaoApi.Testes.Dominio
{
    public class Grupo
    {
        private IMensagensApi _mensagens { get; }
        public Grupo() {
            //Mock da mensageria
            _mensagens = Mock.Of<MensagensApi>();
        }

        [Fact]
        public void ValidacaoNomeCorreto() {
            var dominio = new GrupoDom("Teste", NivelGrupo.Geral, _mensagens);
            dominio.ValidarNome();

            _mensagens.Mensagens.ShouldBeEmpty();
        }

        [Fact]
        public void ValidacaoNomeNumeros() {
            string mensagemErro = "Nome do grupo não pode conter números";
            var dominio = new GrupoDom("teste123", NivelGrupo.Geral, _mensagens);
            dominio.ValidarNome();

            var validacao = _mensagens.Mensagens.Any(m => m.Texto == mensagemErro);
            validacao &= !_mensagens.Mensagens.Any(m => m.Texto != mensagemErro);

            validacao.ShouldBeTrue();
        }

        [Fact]
        public void ValidacaoNivelJustificativa() {
            string mensagemErro = "Grupos cadastrados com nivel acima de Gerente devem possuir justificativa.";
            var dominio = new GrupoDom("Teste", NivelGrupo.Administrador, null, _mensagens);
            dominio.ValidarJustificativaNivel();

            var validacao = _mensagens.Mensagens.Any(m => m.Texto == mensagemErro);

            validacao.ShouldBeTrue();
        }
    }
}