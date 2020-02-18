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
using System;

namespace Testes.Integracao.Grupo
{
    public class GrupoRepTestes : TestesIntegracaoBase, IDisposable
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

            resultado.ShouldBeTrue();
        }

        [Fact]
        public void AdicionarGrupoDadosOpcionaisNulos()
        {
            ExecutarScript("CriarTabelaGrupos", "SHAREDB");

            var grupo = _builder
                .DefinirJustificativaNula()
                .DefinirJustificativaNula()
                .ToDpo();
            var resultado = _repositorio.AdicionarGrupo(grupo);

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

        }

        [Fact]
        public void AtualizarGrupoNivelInferior()
        {
            ExecutarScript("CriarTabelaGrupos", "SHAREDB");

            var grupo = _builder
                .ToDpo();
            _repositorio.AdicionarGrupo(grupo);
            grupo = _builder
                        .DefinirNivelInferior()
                        .DefinirJustificativaNula()
                        .ToDpo();


            var resultado = _repositorio.AtualizarNivelGrupo(grupo.Nome, grupo.Nivel, grupo.Justificativa);
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
            _repositorio.AdicionarGrupo(grupo);
            grupo = _builder
                        .DefinirNivelSuperior()
                        .DefinirJustificativaValida()
                        .ToDpo();


            var resultado = _repositorio.AtualizarNivelGrupo(grupo.Nome, grupo.Nivel, grupo.Justificativa);
            resultado.ShouldBeTrue();
        }

        [Fact]
        public void DeletarGrupoCorretamente()
        {
            ExecutarScript("CriarTabelaGrupos", "SHAREDB");
            var grupo = _builder
                .ToDpo();

            _repositorio.AdicionarGrupo(grupo);
            var resultado = _repositorio.DeletarGrupo(grupo.Nome);

            resultado.ShouldBeTrue();
        }

        [Fact]
        public void DeletarGrupoInexistente()
        {
            ExecutarScript("CriarTabelaGrupos", "SHAREDB");
            var grupo = _builder
                .ToDpo();

            var resultado = _repositorio.DeletarGrupo(grupo.Nome);

            resultado.ShouldBeFalse();
        }

        public void Dispose()
        {
            ExecutarScript("DeletarEstruturaBanco", "SHAREDB");
        }
    }
}