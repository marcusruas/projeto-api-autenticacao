using AutenticacaoApi.Testes.Builders;
using MandradePkgs.Mensagens;
using MandradePkgs.Mensagens.Estrutura.Implementacao;
using Moq;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace AutenticacaoApi.Testes.Dominio.Grupo
{
    public class GrupoTestes : TestesUnitariosBase
    {
        private GrupoBuilder _builder { get; }
        public GrupoTestes(ITestOutputHelper output) : base(output)
        {
            _builder = new GrupoBuilder(_mensagens);
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
        public void CriacaoGrupoCorreto()
        {
            var dominio = _builder.Build();

            dominio.ValidarDados();
            _logs.GravarLogs("Criação correta de um grupo", dominio);

            _mensagens.PossuiFalhasValidacao().ShouldBeFalse();
        }

        [Fact]
        public void CriacaoGrupoIncorreto()
        {
            var dominio = _builder.DefinirGrupoComDadosInvalidos().Build();

            dominio.ValidarDados();
            _logs.GravarLogs("Criação Incorreta de um grupo", dominio);

            _mensagens.Mensagens.Count.ShouldBe(4);
            _mensagens.PossuiFalhasValidacao().ShouldBeTrue();
        }

        [Fact]
        public void CriacaoGrupoNomeCurto()
        {
            var dominio = _builder.DefinirNomeCurto().Build();

            dominio.ValidarDados();
            _logs.GravarLogs("Criação de um grupo com nome inferior a 5 caractéres.", dominio);

            _mensagens.Mensagens.Count.ShouldBe(1);
            _mensagens.PossuiFalhasValidacao().ShouldBeTrue();
        }

        [Fact]
        public void CriacaoGrupoNomeNumeros()
        {
            var dominio = _builder.DefinirNomeComNumeros().Build();

            dominio.ValidarDados();
            _logs.GravarLogs("Criação de um grupo com nome contendo números.", dominio);

            _mensagens.Mensagens.Count.ShouldBe(1);
            _mensagens.PossuiFalhasValidacao().ShouldBeTrue();
        }

        [Fact]
        public void CriacaoGrupoDescricaoInvalida()
        {
            var dominio = _builder.DefinirDescricaoInvalida().Build();

            dominio.ValidarDados();
            _logs.GravarLogs("Criação de um grupo com descrição inválida.", dominio);

            _mensagens.Mensagens.Count.ShouldBe(1);
            _mensagens.PossuiFalhasValidacao().ShouldBeTrue();
        }

        [Fact]
        public void CriacaoGrupoNivelSuperiorSemJustificativa()
        {
            var dominio = _builder
                .DefinirJustificativaNula()
                .DefinirNivelSuperior()
                .Build();

            dominio.ValidarDados();
            _logs.GravarLogs("Criação de um grupo com superior a gerente sem justificativa.", dominio);

            _mensagens.Mensagens.Count.ShouldBe(1);
            _mensagens.PossuiFalhasValidacao().ShouldBeTrue();
        }

        [Fact]
        public void CriacaoGrupoNivelInferiorComJustificativa()
        {
            var dominio = _builder
                .DefinirJustificativaValida()
                .DefinirNivelInferior()
                .Build();

            dominio.ValidarDados();
            _logs.GravarLogs("Criação de um grupo com inferior a gerente com justificativa.", dominio);

            _mensagens.Mensagens.Count.ShouldBe(1);
            _mensagens.PossuiFalhasValidacao().ShouldBeTrue();
        }

        [Fact]
        public void CriacaoGrupoJustificativaInvalida()
        {
            var dominio = _builder.DefinirJustificativaInvalida().Build();

            dominio.ValidarDados();
            _logs.GravarLogs("Criação de um grupo com justificativa inválida", dominio);

            _mensagens.Mensagens.Count.ShouldBe(1);
            _mensagens.PossuiFalhasValidacao().ShouldBeTrue();
        }
    }
}