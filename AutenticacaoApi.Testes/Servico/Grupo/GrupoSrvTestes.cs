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
    public class GrupoSrvTestes
    {
        private IMensagensApi _mensagens { get; }
        private TestLogger _logs { get; }
        private GrupoDomBuilder _builder { get; }
        private Mock<IGrupoRep> _repositorio { get; }
        private IGrupoSrv _servico { get; }
        public GrupoSrvTestes(ITestOutputHelper output) {
            _mensagens = Mock.Of<MensagensApi>();
            _logs = new TestLogger(_mensagens, output);
            _builder = new GrupoDomBuilder(_mensagens);

            _repositorio = new Mock<IGrupoRep>();
            _servico = new GrupoSrv(_repositorio.Object, 
                                    new Mapper(Mapeamentos.DefinirConfiguracoesMapeamento()), 
                                    _mensagens); 
        }

        [Fact]
        public void DeveAdicionarGrupo(){
            var grupo = _builder.Build();
            var dto = new GrupoDto();
            dto.Nome = grupo.Nome;
            dto.Descricao = grupo.Descricao;
            dto.Nivel = grupo.Nivel;
            dto.Justificativa = grupo.Justificativa;
            _servico.InserirNovoUsuario(dto);
            _repositorio.Verify(r => r.AdicionarGrupo(It.IsAny<GrupoDbo>()));
        }
    }
}