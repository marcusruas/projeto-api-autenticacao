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

        [Display(Name = "Criação de grupo de nome com menos de 5 caractéres")]
        [Fact]
        public void TesteCriacaoGrupoDeNomeCurto() {
            var dominio = new GrupoDom(
                    "test",
                    null,
                    NivelGrupo.Geral,
                    _mensagens
            );

            dominio.ValidarNome();

            _mensagens.PossuiFalhasValidacao().ShouldBeTrue();
        }

        [Display(Name = "Criação de um grupo com números no nome")]
        [Fact]
        public void TesteCriacaoGrupoDeNomeComNumeros() {
            var dominio = new GrupoDom(
                    "Teste123",
                    null,
                    NivelGrupo.Geral,
                    _mensagens
            );

            dominio.ValidarNome();

            _mensagens.PossuiFalhasValidacao().ShouldBeTrue();
        }

        [Display(Name = "Criação de um grupo com justificativa de nível inferior a gerente")]
        [Fact]
        public void TesteCriacaoGrupoDeJustificativaSemNivel() {
            var dominio = new GrupoDom(
                    "Testes",
                    null,
                    NivelGrupo.Geral,
                    "Por que eu quero adicionar",
                    _mensagens
            );

            dominio.ValidarJustificativa();
            dominio.ValidarJustificativaParaNivel();

            _mensagens.PossuiFalhasValidacao().ShouldBeTrue();
        }

        [Display(Name = "Criação de um grupo sem justificativa com nível superior a gerente")]
        [Fact]
        public void TesteCriacaoGrupoSemJustificativaComNivel() {
            var dominio = new GrupoDom(
                    "Testes",
                    null,
                    NivelGrupo.Absoluto,
                    null,
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