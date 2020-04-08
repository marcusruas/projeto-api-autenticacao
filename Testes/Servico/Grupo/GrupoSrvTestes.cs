using Testes.Builders;
using AutoMapper;
using MandradePkgs.Mensagens;
using Moq;
using Servico.Usuario.Interfaces;
using Xunit;
using Xunit.Abstractions;
using Shouldly;
using System.Linq;
using MandradePkgs.Retornos.Erros.Exceptions;
using Abstracoes.Representacoes;
using Repositorios.Usuario.Interfaces;
using Servico.Usuario.Implementacoes;
using Abstracoes.Representacoes.Usuario.Grupo;

namespace Testes.Servico.Grupo
{
    public class GrupoSrvTestes : TestesUnitariosBase
    {
        private GrupoBuilder _builder { get; }
        private Mock<IGrupoRep> _Repositorios { get; }
        private IGrupoSrv _servico { get; }
        public GrupoSrvTestes(ITestOutputHelper output) : base(output)
        {
            var mapper = new Mapper(Mapeamentos.DefinirConfiguracoesMapeamento());

            _builder = new GrupoBuilder(_mensagens);
            _Repositorios = new Mock<IGrupoRep>();
            _servico = new GrupoSrv(_Repositorios.Object, mapper, _mensagens);
        }

        [Fact]
        public void AdicionarGrupoCorreto()
        {
            var grupo = _builder.ToDto();
            _Repositorios.Setup(x => x.AdicionarGrupo(It.IsAny<GrupoDpo>())).Returns(true);

            _servico.InserirNovoUsuario(grupo);

            _mensagens.Mensagens.Any(m => m.Tipo == (int)TipoMensagem.Informativo).ShouldBe(true);
            _mensagens.Mensagens.Count.ShouldBe(1);
            _Repositorios.Verify(r => r.AdicionarGrupo(It.IsAny<GrupoDpo>()));
        }

        [Fact]
        public void AdicionarGrupoIncorreto()
        {
            _builder.DefinirGrupoComDadosInvalidos();
            var grupo = _builder.ToDto();
            _Repositorios.Setup(x => x.AdicionarGrupo(It.IsAny<GrupoDpo>())).Returns(true);

            Should.Throw<RegraNegocioException>(() =>
                _servico.InserirNovoUsuario(grupo)
            );
            _mensagens.Mensagens.Count.ShouldBe(4);
        }

        [Fact]
        public void AdicionarGrupoExistente()
        {
            var grupo = _builder.ToDto();
            _Repositorios.Setup(x => x.AdicionarGrupo(It.IsAny<GrupoDpo>())).Returns(false);

            Should.Throw<FalhaExecucaoException>(() =>
                _servico.InserirNovoUsuario(grupo)
            );
        }

        [Fact]
        public void AlterarGrupoComJustificativaInvalida()
        {
            var grupo = _builder
                            .DefinirJustificativaInvalida()
                            .DefinirNivelSuperior()
                            .ToDto();

            Should.Throw<RegraNegocioException>(() =>
                _servico.AtualizarNivelGrupo(grupo.Nome, grupo.Nivel, grupo.Justificativa)
            );

            _mensagens.Mensagens.Any(m => m.Tipo == (int)TipoMensagem.FalhaValidacao).ShouldBeTrue();
            _mensagens.Mensagens.Count.ShouldBe(1);
            _Repositorios.Verify(x => x.AtualizarNivelGrupo(
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<string>()
            ), Times.Never());
        }

        [Fact]
        public void AlterarGrupoComJustificativaInvalidaParaNivel()
        {
            var grupo = _builder
                            .DefinirJustificativaValida()
                            .DefinirNivelInferior()
                            .ToDto();

            Should.Throw<RegraNegocioException>(() =>
                _servico.AtualizarNivelGrupo(grupo.Nome, grupo.Nivel, grupo.Justificativa)
            );

            _mensagens.Mensagens.Any(m => m.Tipo == (int)TipoMensagem.FalhaValidacao).ShouldBeTrue();
            _mensagens.Mensagens.Count.ShouldBe(1);
            _Repositorios.Verify(x => x.AtualizarNivelGrupo(
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<string>()
            ), Times.Never());
        }

        [Fact]
        public void AlterarGrupoCorretamente()
        {
            var grupo = _builder
                            .DefinirJustificativaValida()
                            .DefinirNivelSuperior()
                            .ToDto();
            _Repositorios.Setup(x => x.AtualizarNivelGrupo(
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<string>())
            ).Returns(true);

            _servico.AtualizarNivelGrupo(grupo.Nome, grupo.Nivel, grupo.Justificativa);

            _mensagens.Mensagens.Any(m => m.Tipo == (int)TipoMensagem.Informativo).ShouldBeTrue();
            _mensagens.Mensagens.Any(m => m.Tipo == (int)TipoMensagem.FalhaValidacao).ShouldBeFalse();
            _Repositorios.Verify(x => x.AtualizarNivelGrupo(
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<string>()), Times.Once
            );
        }
    }
}