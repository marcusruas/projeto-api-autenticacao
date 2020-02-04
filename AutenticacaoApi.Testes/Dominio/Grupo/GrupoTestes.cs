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
        private readonly ITestOutputHelper _output;
        private IMensagensApi _mensagens { get; }
        private GrupoBuilder _dadosGrupo { get; }
        public GrupoTestes(ITestOutputHelper output) {
            _mensagens = Mock.Of<MensagensApi>();
            _dadosGrupo = new GrupoBuilder(_mensagens);
            _output = output;
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
            _output.WriteLine("Teste: Criação correta de um grupo");
            var dominio = _dadosGrupo.Build();

            dominio.ValidarDados();
            _output.WriteLine(ConfigurarMensagemDados(dominio));
            _output.WriteLine(ConfigurarMensageria());

            _mensagens.PossuiFalhasValidacao().ShouldBeFalse();
        }

        [Fact]
        public void CriacaoGrupoIncorreto() {
            _output.WriteLine("Teste: Criação Incorreta de um grupo");
            var dominio = _dadosGrupo.DefinirGrupoComDadosInvalidos().Build();

            dominio.ValidarDados();
            _output.WriteLine(ConfigurarMensagemDados(dominio));
            _output.WriteLine(ConfigurarMensageria());

            _mensagens.Mensagens.Count.ShouldBe(4);
            _mensagens.PossuiFalhasValidacao().ShouldBeTrue();
        }

        [Fact]
        public void CriacaoGrupoNomeCurto(){
            _output.WriteLine("Teste: Criação de um grupo com nome inferior a 5 caractéres.");
            var dominio = _dadosGrupo.DefinirNomeCurto().Build();

            dominio.ValidarDados();
            _output.WriteLine(ConfigurarMensagemDados(dominio));
            _output.WriteLine(ConfigurarMensageria());

            _mensagens.Mensagens.Count.ShouldBe(1);
            _mensagens.PossuiFalhasValidacao().ShouldBeTrue();
        }

        [Fact]
        public void CriacaoGrupoNomeNumeros(){
            _output.WriteLine("Teste: Criação de um grupo com nome contendo números.");
            var dominio = _dadosGrupo.DefinirNomeComNumeros().Build();

            dominio.ValidarDados();
            _output.WriteLine(ConfigurarMensagemDados(dominio));
            _output.WriteLine(ConfigurarMensageria());

            _mensagens.Mensagens.Count.ShouldBe(1);
            _mensagens.PossuiFalhasValidacao().ShouldBeTrue();
        }

        [Fact]
        public void CriacaoGrupoDescricaoInvalida(){
            _output.WriteLine("Teste: Criação de um grupo com descrição inválida.");
            var dominio = _dadosGrupo.DefinirDescricaoInvalida().Build();

            dominio.ValidarDados();
            _output.WriteLine(ConfigurarMensagemDados(dominio));
            _output.WriteLine(ConfigurarMensageria());

            _mensagens.Mensagens.Count.ShouldBe(1);
            _mensagens.PossuiFalhasValidacao().ShouldBeTrue();
        }

        [Fact]
        public void CriacaoGrupoNivelSuperiorSemJustificativa(){
            _output.WriteLine("Teste: Criação de um grupo com superior a gerente sem justificativa.");
            var dominio = _dadosGrupo
                            .DefinirJustificativaNula()
                            .DefinirNivelSuperior()
                            .Build();

            dominio.ValidarDados();
            _output.WriteLine(ConfigurarMensagemDados(dominio));
            _output.WriteLine(ConfigurarMensageria());

            _mensagens.Mensagens.Count.ShouldBe(1);
            _mensagens.PossuiFalhasValidacao().ShouldBeTrue();
        }

        [Fact]
        public void CriacaoGrupoNivelInferiorComJustificativa(){
            _output.WriteLine("Teste: Criação de um grupo com inferior a gerente com justificativa.");
            var dominio = _dadosGrupo
                            .DefinirJustificativaValida()
                            .DefinirNivelInferior()
                            .Build();

            dominio.ValidarDados();
            _output.WriteLine(ConfigurarMensagemDados(dominio));
            _output.WriteLine(ConfigurarMensageria());

            _mensagens.Mensagens.Count.ShouldBe(1);
            _mensagens.PossuiFalhasValidacao().ShouldBeTrue();
        }

        [Fact]
        public void CriacaoGrupoJustificativaInvalida(){
            _output.WriteLine("Teste: Criação de um grupo com justificativa inválida");
            var dominio = _dadosGrupo.DefinirJustificativaInvalida().Build();

            dominio.ValidarDados();
            _output.WriteLine(ConfigurarMensagemDados(dominio));
            _output.WriteLine(ConfigurarMensageria());

            _mensagens.Mensagens.Count.ShouldBe(1);
            _mensagens.PossuiFalhasValidacao().ShouldBeTrue();
        }

        private string ConfigurarMensagemDados(GrupoDom grupo){
            return $@"
                Nome: {grupo.Nome},
                Nivel: {grupo.Nivel},
                Descricao: {grupo.Descricao},
                Justificativa: {grupo.Justificativa}
            ";
        }

        private string ConfigurarMensageria(){
            var stringCompleta = "Mensagens: \n";

            foreach(var msg in _mensagens.Mensagens)
                stringCompleta += "\t"+msg.Texto+"\n";

            return stringCompleta;
        }
    }
}