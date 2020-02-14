using System.Data.SqlClient;
using Testes;
using Testes.Builders;
using Dapper;
using MandradePkgs.Conexoes;
using MandradePkgs.Conexoes.Estrutura.Implementacao;
using Repositorio.Grupo.Implementacao;
using Repositorio.Grupo.Interface;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Testes.Integracao.Grupo
{
    public class GrupoRepTestes : TestesIntegracaoBase
    {
        private GrupoBuilder _builder { get; }
        private IGrupoRep _repositorio { get; }

        public GrupoRepTestes(ITestOutputHelper output) : base(output)
        {
            _repositorio = new GrupoRep(_conexao);
            _builder = new GrupoBuilder(_mensagens);
        }

        [Fact]
        public void DeveAdicionarGrupo()
        {
            ExecutarScript("CriarTabelaGrupos", "SHAREDB");

            var grupo = _builder.ToDpo();
            var resultado = _repositorio.AdicionarGrupo(grupo);
            ExecutarScript("DeletarEstruturaBanco", "SHAREDB");

            resultado.ShouldBeTrue();
        }

        [Fact]
        public void AdicionarGrupoDadosNulos()
        {
            ExecutarScript("CriarTabelaGrupos", "SHAREDB");

            var grupo = _builder
                .DefinirJustificativaNula()
                .DefinirJustificativaNula()
                .ToDpo();
            var resultado = _repositorio.AdicionarGrupo(grupo);
            ExecutarScript("DeletarEstruturaBanco", "SHAREDB");

            resultado.ShouldBeTrue();
        }

        [Fact]
        public void AdicionarGrupoDadosObrigatoriosNulos()
        {
            ExecutarScript("CriarTabelaGrupos", "SHAREDB");

            var grupo = _builder
                .DefinirNomeNulo()
                .DefinirNivelInferior()
                .ToDpo();
            Should.Throw<SqlException>(() =>
                _repositorio.AdicionarGrupo(grupo)
            );

            ExecutarScript("DeletarEstruturaBanco", "SHAREDB");
        }
    }
}