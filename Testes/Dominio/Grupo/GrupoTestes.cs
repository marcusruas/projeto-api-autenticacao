using Testes.Builders;
using Shouldly;
using Xunit;
using Xunit.Abstractions;
using Dominio.Representacao.Usuario.Grupo;

namespace Testes.dominio.Grupo
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

            _mensagens.PossuiFalhasValidacao().ShouldBeFalse();
        }

        [Fact]
        public void CriacaoGrupoIncorreto()
        {
            var dominio = _builder.DefinirGrupoComDadosInvalidos().Build();

            dominio.ValidarDados();

            _mensagens.Mensagens.Count.ShouldBe(4);
            _mensagens.PossuiFalhasValidacao().ShouldBeTrue();
        }

        [Fact]
        public void CriacaoGrupoNomeCurto()
        {
            var dominio = _builder.DefinirNomeCurto().Build();

            dominio.ValidarDados();

            _mensagens.Mensagens.Count.ShouldBe(1);
            _mensagens.PossuiFalhasValidacao().ShouldBeTrue();
        }

        [Fact]
        public void CriacaoGrupoNomeNumeros()
        {
            var dominio = _builder.DefinirNomeComNumeros().Build();

            dominio.ValidarDados();

            _mensagens.Mensagens.Count.ShouldBe(1);
            _mensagens.PossuiFalhasValidacao().ShouldBeTrue();
        }

        [Fact]
        public void CriacaoGrupoDescricaoInvalida()
        {
            var dominio = _builder.DefinirDescricaoInvalida().Build();

            dominio.ValidarDados();

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

            _mensagens.Mensagens.Count.ShouldBe(1);
            _mensagens.PossuiFalhasValidacao().ShouldBeTrue();
        }

        [Fact]
        public void CriacaoGrupoJustificativaInvalida()
        {
            var dominio = _builder.DefinirJustificativaInvalida().Build();

            dominio.ValidarDados();

            _mensagens.Mensagens.Count.ShouldBe(1);
            _mensagens.PossuiFalhasValidacao().ShouldBeTrue();
        }

        [Fact]
        public void MapeamentoGrupoParaDto()
        {
            GrupoDto objeto = _builder.DefinirJustificativaInvalida().ToDto();

            objeto.Nome.ShouldNotBeNull();
            objeto.Nivel.ShouldNotBeNull();
            objeto.Descricao.ShouldNotBeNull();
            objeto.Justificativa.ShouldNotBeNull();
            objeto.Id.ShouldNotBeNull();
        }
    }
}