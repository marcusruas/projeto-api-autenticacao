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
            _mensagens = Mock.Of<MensagensApi>();
        }

        [Fact]
        public void ValidacaoNomeGrupoValido() {
            var dominio = new GrupoDom("Teste", NivelGrupo.Geral, _mensagens);
            
            dominio.ValidarNome();

            _mensagens.Mensagens.ShouldBeEmpty();
        }

        [Fact]
        public void ValidacaoNomeGrupoNumeros() {
            string mensagemErro = "Nome do grupo não pode conter números";
            var dominio = new GrupoDom("teste123", NivelGrupo.Geral, _mensagens);
            
            dominio.ValidarNome();

            var validacao = _mensagens.Mensagens.Any(m => m.Texto == mensagemErro);
            validacao &= !_mensagens.Mensagens.Any(m => m.Texto != mensagemErro);
            validacao.ShouldBeTrue();
        }

        [Fact]
        public void ValidacaoNivelSemJustificativa() {
            string mensagemErro = "Grupos cadastrados com nivel acima de Gerente devem possuir justificativa.";
            var dominio = new GrupoDom("Teste", NivelGrupo.Administrador, null, _mensagens);
            
            dominio.ValidarJustificativaNivel();

            var validacao = _mensagens.Mensagens.Any(m => m.Texto == mensagemErro);
            validacao.ShouldBeTrue();
        }
    }
}