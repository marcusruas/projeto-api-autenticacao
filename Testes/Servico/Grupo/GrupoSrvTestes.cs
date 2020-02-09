using Aplicacao;
using Aplicacao.Grupo;
using AutenticacaoApi.Testes.Builders;
using AutoMapper;
using MandradePkgs.Mensagens;
using MandradePkgs.Mensagens.Estrutura.Implementacao;
using Moq;
using Repositorio.Grupo.Interface;
using Servico.Grupo.Implementacao;
using Servico.Grupo.Interface;
using Xunit;
using Xunit.Abstractions;
using Shouldly;

namespace AutenticacaoApi.Testes.Servico.Grupo
{
    public class GrupoSrvTestes : TestesUnitariosBase
    {
        private GrupoDomBuilder _builder { get; }
        private Mock<IGrupoRep> _repositorio { get; }
        private IGrupoSrv _servico { get; }
        public GrupoSrvTestes(ITestOutputHelper output) : base(output)
        {
            _builder = new GrupoDomBuilder(_mensagens);

            var mapper = new Mock<IMapper>();
            _repositorio = new Mock<IGrupoRep>();
            _repositorio.Setup(x => x.AdicionarGrupo(It.IsAny<GrupoDpo>())).Returns(true);
            _servico = new GrupoSrv(_repositorio.Object,
                                    mapper.Object,
                                    _mensagens);
        }

        [Fact]
        public void DeveAdicionarGrupo()
        {
            var grupo = _builder.Build();
            var dto = new GrupoDto();
            dto.Nome = grupo.Nome;
            dto.Descricao = grupo.Descricao;
            dto.Nivel = grupo.Nivel;
            dto.Justificativa = grupo.Justificativa;
            _servico.InserirNovoUsuario(dto);
            _repositorio.Verify(r => r.AdicionarGrupo(It.IsAny<GrupoDpo>()));
        }
    }
}