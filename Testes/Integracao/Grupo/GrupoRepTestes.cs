using System.Data.SqlClient;
using Testes.Builders;
using Shouldly;
using Xunit;
using Xunit.Abstractions;
using System;
using ExpectedObjects;
using Repositorios.Usuario.Interfaces;
using Repositorios.Usuario.Implementacoes;

namespace Testes.Integracao.Grupo
{
    public class GrupoRepTestes : TestesIntegracaoBase, IDisposable
    {
        private GrupoBuilder _builder { get; }
        private IGrupoRep _Repositorios { get; }

        public GrupoRepTestes(ITestOutputHelper output) : base(output)
        {
            _Repositorios = new GrupoRep(_conexao);
            _builder = new GrupoBuilder(_mensagens);
        }

        [Fact]
        public void DeveAdicionarGrupo()
        {
            var grupo = _builder.ToDpo();
            ExecutarScript("CriarTabelaGrupos", "SHAREDB");

            var resultado = _Repositorios.AdicionarGrupo(grupo);
            var grupoAdicionado = _Repositorios.ObterDadosGrupo(grupo.Nome);
            grupo.Id = grupoAdicionado.Id;

            resultado.ShouldBeTrue();
            grupoAdicionado.ToExpectedObject().ShouldEqual(grupo);
        }

        [Fact]
        public void AdicionarGrupoDadosOpcionaisNulos()
        {
            ExecutarScript("CriarTabelaGrupos", "SHAREDB");
            var grupo = _builder
                .DefinirJustificativaNula()
                .DefinirJustificativaNula()
                .ToDpo();

            var resultado = _Repositorios.AdicionarGrupo(grupo);
            var grupoAdicionado = _Repositorios.ObterDadosGrupo(grupo.Nome);
            grupo.Id = grupoAdicionado.Id;

            resultado.ShouldBeTrue();
            grupoAdicionado.ToExpectedObject().ShouldEqual(grupo);
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
                _Repositorios.AdicionarGrupo(grupo)
            );

        }

        [Fact]
        public void AtualizarGrupoNivelInferior()
        {
            ExecutarScript("CriarTabelaGrupos", "SHAREDB");

            var grupo = _builder
                .ToDpo();
            _Repositorios.AdicionarGrupo(grupo);
            grupo = _builder.DefinirNivelInferior().DefinirJustificativaNula().ToDpo();

            var resultado = _Repositorios.AtualizarNivelGrupo(grupo.Nome, grupo.Nivel, grupo.Justificativa);

            resultado.ShouldBeTrue();
        }

        [Fact]
        public void AtualizarGrupoNivelSuperior()
        {
            ExecutarScript("CriarTabelaGrupos", "SHAREDB");

            var grupo = _builder
                        .DefinirNivelInferior()
                        .DefinirJustificativaNula()
                        .ToDpo();
            _Repositorios.AdicionarGrupo(grupo);
            grupo = _builder
                        .DefinirNivelSuperior()
                        .DefinirJustificativaValida()
                        .ToDpo();


            var resultado = _Repositorios.AtualizarNivelGrupo(grupo.Nome, grupo.Nivel, grupo.Justificativa);
            resultado.ShouldBeTrue();
        }

        [Fact]
        public void DeletarGrupoCorretamente()
        {
            ExecutarScript("CriarTabelaGrupos", "SHAREDB");
            var grupo = _builder
                .ToDpo();

            _Repositorios.AdicionarGrupo(grupo);
            var resultado = _Repositorios.DeletarGrupo(grupo.Nome);

            resultado.ShouldBeTrue();
        }

        [Fact]
        public void DeletarGrupoInexistente()
        {
            ExecutarScript("CriarTabelaGrupos", "SHAREDB");
            var grupo = _builder
                .ToDpo();

            var resultado = _Repositorios.DeletarGrupo(grupo.Nome);

            resultado.ShouldBeFalse();
        }

        public void Dispose()
        {
            ExecutarScript("DeletarEstruturaBanco", "SHAREDB");
        }
    }
}