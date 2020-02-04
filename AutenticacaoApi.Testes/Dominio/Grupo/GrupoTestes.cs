using System;
using Dominio.Grupo;
using MandradePkgs.Mensagens;
using MandradePkgs.Mensagens.Estrutura.Implementacao;
using Moq;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace AutenticacaoApi.Testes.Dominio.Grupo
{
    public class GrupoTestes
    {
        private IMensagensApi _mensagens { get; }
        private GrupoBuilder _builder { get; }
        private GrupoTestesLogs _logger { get; }
        public GrupoTestes(ITestOutputHelper output) {
            _mensagens = Mock.Of<MensagensApi>();
            _builder = new GrupoBuilder(_mensagens);
            _logger = new GrupoTestesLogs(_mensagens, output, _builder);
        }

        /*
            *Nome do grupo não pode ser nulo;
            *Nome do grupo deve conter mais de 5 caractéres;
            *Nome do grupo não deve conter números;
            *Grupos com nível acima de gerente devem possuir justificativa;
            *Grupos com nível abaixo de gerente não necessitam justificativa;
            *Se o grupo tiver descrição, a mesma deve possuir ao menos 15 caractéres;
            *Se o grupo tiver Justificativa, a mesma deve possuir ao menos 15 caractéres.
        */        

        [Fact]
        public void CriacaoGrupoCorreto() {
            _logger.DefinirTeste("Teste: Criação correta de um grupo");
            var dominio = _builder.Build();

            dominio.ValidarDados();
            _logger.GravarLogs();

            _mensagens.PossuiFalhasValidacao().ShouldBeFalse();
        }

        [Fact]
        public void CriacaoGrupoIncorreto() {
            _logger.DefinirTeste("Teste: Criação Incorreta de um grupo");
            var dominio = _builder.DefinirGrupoComDadosInvalidos().Build();

            dominio.ValidarDados();
            _logger.GravarLogs();

            _mensagens.Mensagens.Count.ShouldBe(4);
            _mensagens.PossuiFalhasValidacao().ShouldBeTrue();
        }

        [Fact]
        public void CriacaoGrupoNomeCurto(){
            _logger.DefinirTeste("Teste: Criação de um grupo com nome inferior a 5 caractéres.");
            var dominio = _builder.DefinirNomeCurto().Build();

            dominio.ValidarDados();
            _logger.GravarLogs();

            _mensagens.Mensagens.Count.ShouldBe(1);
            _mensagens.PossuiFalhasValidacao().ShouldBeTrue();
        }

        [Fact]
        public void CriacaoGrupoNomeNumeros(){
            _logger.DefinirTeste("Teste: Criação de um grupo com nome contendo números.");
            var dominio = _builder.DefinirNomeComNumeros().Build();

            dominio.ValidarDados();
            _logger.GravarLogs();

            _mensagens.Mensagens.Count.ShouldBe(1);
            _mensagens.PossuiFalhasValidacao().ShouldBeTrue();
        }

        [Fact]
        public void CriacaoGrupoDescricaoInvalida(){
            _logger.DefinirTeste("Teste: Criação de um grupo com descrição inválida.");
            var dominio = _builder.DefinirDescricaoInvalida().Build();

            dominio.ValidarDados();
            _logger.GravarLogs();

            _mensagens.Mensagens.Count.ShouldBe(1);
            _mensagens.PossuiFalhasValidacao().ShouldBeTrue();
        }

        [Fact]
        public void CriacaoGrupoNivelSuperiorSemJustificativa(){
            _logger.DefinirTeste("Teste: Criação de um grupo com superior a gerente sem justificativa.");
            var dominio = _builder
                            .DefinirJustificativaNula()
                            .DefinirNivelSuperior()
                            .Build();

            dominio.ValidarDados();
            _logger.GravarLogs();

            _mensagens.Mensagens.Count.ShouldBe(1);
            _mensagens.PossuiFalhasValidacao().ShouldBeTrue();
        }

        [Fact]
        public void CriacaoGrupoNivelInferiorComJustificativa(){
            _logger.DefinirTeste("Teste: Criação de um grupo com inferior a gerente com justificativa.");
            var dominio = _builder
                            .DefinirJustificativaValida()
                            .DefinirNivelInferior()
                            .Build();

            dominio.ValidarDados();
            _logger.GravarLogs();

            _mensagens.Mensagens.Count.ShouldBe(1);
            _mensagens.PossuiFalhasValidacao().ShouldBeTrue();
        }

        [Fact]
        public void CriacaoGrupoJustificativaInvalida(){
            _logger.DefinirTeste("Teste: Criação de um grupo com justificativa inválida");
            var dominio = _builder.DefinirJustificativaInvalida().Build();

            dominio.ValidarDados();
            _logger.GravarLogs();

            _mensagens.Mensagens.Count.ShouldBe(1);
            _mensagens.PossuiFalhasValidacao().ShouldBeTrue();
        }
    }
}