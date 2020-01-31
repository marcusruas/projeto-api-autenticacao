using Dominio.Grupo;
using MandradePkgs.Mensagens;
using MandradePkgs.Mensagens.Estrutura.Implementacao;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        /*
            *Nome do grupo deve conter mais de 5 caractéres;
            *Nome do grupo não deve conter números;
            *Grupos com nível acima de gerente devem possuir justificativa;
            *Grupos com nível abaixo de gerente não necessitam justificativa;
            *Se o grupo tiver descrição, a mesma deve possuir ao menos 15 caractéres;
            *Se o grupo tiver Justificativa, a mesma deve possuir ao menos 15 caractéres.
        */        

        [Display(Name = "Criação correta de um grupo")]
        [Fact]
        public void TesteCriacaoDeGrupoCorreta() {
            var dominio = new GrupoDom(
                    "Moderador",
                    "Responsável por mediar conflitos entre jogadores",
                    NivelGrupo.Administrador,
                    "Moderador deve acessar funções internas do jogo para desempenhar sua função",
                    _mensagens
            );

            dominio.ValidarNome();
            dominio.ValidarDescricao();
            dominio.ValidarJustificativa();
            dominio.ValidarJustificativaParaNivel();

            _mensagens.PossuiFalhasValidacao().ShouldBeFalse();
        }

        [Display(Name = "Criação de grupo de nome com menos de 5 caractéres ou com Números")]
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("test")]
        [InlineData("Teste123")]
        public void TesteCriacaoGrupoDeNomeInvalido(string nome) {
            var dominio = new GrupoDom(
                    nome,
                    null,
                    NivelGrupo.Geral,
                    _mensagens
            );

            dominio.ValidarNome();

            _mensagens.PossuiFalhasValidacao().ShouldBeTrue();
        }

        [Display(Name = "Criação de um grupo com justificativa inválida")]
        [Theory]
        [InlineData(NivelGrupo.Geral, "Por que eu quero adicionar")]
        [InlineData(NivelGrupo.Absoluto, null)]
        [InlineData(NivelGrupo.Absoluto, "Por que sim")]
        public void TesteCriacaoGrupoDeJustificativaInvalida(NivelGrupo nivel, string justificativa) {
            var dominio = new GrupoDom(
                    "Testes",
                    null,
                    nivel,
                    justificativa,
                    _mensagens
            );

            dominio.ValidarJustificativa();
            dominio.ValidarJustificativaParaNivel();

            _mensagens.PossuiFalhasValidacao().ShouldBeTrue();
        }

        [Display(Name = "Criação de um grupo com Descrição inválida")]
        [Fact]
        public void TesteCriacaoGrupoComDescricaoInválida() {
            var dominio = new GrupoDom(
                    "Testes",
                    "teste.",
                    NivelGrupo.Absoluto,
                    _mensagens
            );

            dominio.ValidarDescricao();

            _mensagens.PossuiFalhasValidacao().ShouldBeTrue();
        }
    }
}