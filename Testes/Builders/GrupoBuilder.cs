using System.Linq;
using AutoMapper;
using Bogus;
using Dominio.Logica.Grupo;
using Dominio.ObjetosValor.Grupo;
using Dominio.Representacao;
using Dominio.Representacao.Grupo;
using MandradePkgs.Mensagens;

namespace Testes.Builders
{
    public class GrupoBuilder
    {
        private Faker _faker { get; }
        private GrupoDom Grupo;
        private IMensagensApi _mensagens;
        private IMapper _mapper;

        public GrupoBuilder(IMensagensApi mensagens)
        {
            _faker = new Faker();
            _mensagens = mensagens;
            _mapper = new Mapper(Mapeamentos.DefinirConfiguracoesMapeamento());
            Grupo = CriacaoGrupoDadosValidos();
        }

        public GrupoDom CriacaoGrupoDadosValidos() =>
            new GrupoDom(
                _faker.Name.FullName(),
                new string(_faker.Lorem.Paragraph().Take(GrupoDom.LimiteCaracteresDescricao).ToArray()),
                NivelGrupo.Administrador,
                new string(_faker.Lorem.Paragraph().Take(GrupoDom.LimiteCaracteresJustificativa).ToArray()),
                _mensagens
            );

        public GrupoBuilder DefinirGrupoComDadosInvalidos()
        {
            Grupo = new GrupoDom(
                _faker.Name.FullName().Substring(0, 5),
                _faker.Random.String(10),
                NivelGrupo.Geral,
                _faker.Random.String(10),
                _mensagens
            );
            return this;
        }

        public GrupoBuilder DefinirNomeCurto()
        {
            var grupoAlterado = new GrupoDom(
                _faker.Name.FullName().Substring(0, 5),
                Grupo.Descricao,
                Grupo.Nivel,
                Grupo.Justificativa,
                _mensagens
            );

            Grupo = grupoAlterado;

            return this;
        }

        public GrupoBuilder DefinirNomeNulo()
        {
            var grupoAlterado = new GrupoDom(
                null,
                Grupo.Descricao,
                Grupo.Nivel,
                Grupo.Justificativa,
                _mensagens
            );

            Grupo = grupoAlterado;

            return this;
        }

        public GrupoBuilder DefinirNomeComNumeros()
        {
            var grupoAlterado = new GrupoDom(
                _faker.Name.FullName().Insert(0, _faker.Random.Int(1, 5).ToString()),
                Grupo.Descricao,
                Grupo.Nivel,
                Grupo.Justificativa,
                _mensagens
            );

            Grupo = grupoAlterado;

            return this;
        }

        public GrupoBuilder DefinirDescricaoInvalida()
        {
            var grupoAlterado = new GrupoDom(
                Grupo.Nome,
                _faker.Lorem.Paragraph().Substring(0, 10),
                Grupo.Nivel,
                Grupo.Justificativa,
                _mensagens
            );

            Grupo = grupoAlterado;

            return this;
        }

        public GrupoBuilder DefinirNivelInferior()
        {
            var grupoAlterado = new GrupoDom(
                Grupo.Nome,
                Grupo.Descricao,
                NivelGrupo.Geral,
                Grupo.Justificativa,
                _mensagens
            );

            Grupo = grupoAlterado;

            return this;
        }

        public GrupoBuilder DefinirNivelSuperior()
        {
            var grupoAlterado = new GrupoDom(
                Grupo.Nome,
                Grupo.Descricao,
                NivelGrupo.Absoluto,
                Grupo.Justificativa,
                _mensagens
            );

            Grupo = grupoAlterado;

            return this;
        }

        public GrupoBuilder DefinirJustificativaValida()
        {
            var grupoAlterado = new GrupoDom(
                Grupo.Nome,
                Grupo.Descricao,
                Grupo.Nivel,
                new string(_faker.Lorem.Paragraph().Take(GrupoDom.LimiteCaracteresJustificativa).ToArray()),
                _mensagens
            );

            Grupo = grupoAlterado;

            return this;
        }

        public GrupoBuilder DefinirJustificativaInvalida()
        {
            var grupoAlterado = new GrupoDom(
                Grupo.Nome,
                Grupo.Descricao,
                Grupo.Nivel,
                _faker.Lorem.Paragraph().Substring(0, 10),
                _mensagens
            );

            Grupo = grupoAlterado;

            return this;
        }

        public GrupoBuilder DefinirJustificativaNula()
        {
            var grupoAlterado = new GrupoDom(
                Grupo.Nome,
                Grupo.Descricao,
                Grupo.Nivel,
                null,
                _mensagens
            );

            Grupo = grupoAlterado;

            return this;
        }

        public GrupoDom Build() => Grupo;

        public GrupoDto ToDto() =>
            _mapper.Map<GrupoDto>(Grupo);

        public GrupoDpo ToDpo() =>
            _mapper.Map<GrupoDpo>(Grupo);
    }
}