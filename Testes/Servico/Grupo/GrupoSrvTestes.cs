using Aplicacao;
using Aplicacao.Grupo;
using AutenticacaoApi.Testes.Builders;
using AutoMapper;
using MandradePkgs.Mensagens;
using Moq;
using Repositorio.Grupo.Interface;
using Servico.Grupo.Implementacao;
using Servico.Grupo.Interface;
using Xunit;
using Xunit.Abstractions;
using Shouldly;
using System.Linq;

namespace AutenticacaoApi.Testes.Servico.Grupo
{
    public class GrupoSrvTestes : TestesUnitariosBase
    {
        private GrupoBuilder _builder { get; }
        private Mock<IGrupoRep> _repositorio { get; }
        private IGrupoSrv _servico { get; }
        public GrupoSrvTestes(ITestOutputHelper output) : base(output)
        {
            var mapper = new Mapper(Mapeamentos.DefinirConfiguracoesMapeamento());

            _builder = new GrupoBuilder(_mensagens);
            _repositorio = new Mock<IGrupoRep>();
            _servico = new GrupoSrv(_repositorio.Object, mapper, _mensagens);
        }

        [Fact]
        public void DeveAdicionarGrupo()
        {
            var grupo = _builder.ToDto();
            _repositorio.Setup(x => x.AdicionarGrupo(It.IsAny<GrupoDpo>())).Returns(true);

            _servico.InserirNovoUsuario(grupo);

            _mensagens.Mensagens.Any(m => m.Tipo == (int)TipoMensagem.Informativo).ShouldBe(true);
            _mensagens.Mensagens.Count.ShouldBe(1);
            _repositorio.Verify(r => r.AdicionarGrupo(It.IsAny<GrupoDpo>()));
        }
    }
}