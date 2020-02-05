using Bogus;
using Dominio.Grupo;
using MandradePkgs.Mensagens;

namespace AutenticacaoApi.Testes.Builders
{
    public class GrupoDomBuilder
    {
        private Faker _faker { get; }
        private GrupoDom Grupo;
        private IMensagensApi _mensagens;
        public GrupoDomBuilder(IMensagensApi mensagens)
        {
            _faker = new Faker();
            _mensagens = mensagens;
            Grupo = CriacaoGrupoDadosValidos();
        }

        public GrupoDom CriacaoGrupoDadosValidos() =>
            new GrupoDom(
                _faker.Name.FullName(),
                _faker.Lorem.Paragraph(),
                NivelGrupo.Administrador,
                _faker.Lorem.Paragraph(),
                _mensagens
            );

        public GrupoDomBuilder DefinirGrupoComDadosInvalidos() {
            Grupo = new GrupoDom(
                _faker.Name.FullName().Substring(0, 5),
                _faker.Random.String(10),
                NivelGrupo.Geral,
                _faker.Random.String(10),
                _mensagens
            );

            return this;
        }

        public GrupoDomBuilder DefinirNomeCurto() {
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

        public GrupoDomBuilder DefinirNomeNulo() {
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

        public GrupoDomBuilder DefinirNomeComNumeros() {
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

        public GrupoDomBuilder DefinirDescricaoValida() {
            var grupoAlterado = new GrupoDom(
                Grupo.Nome,
                _faker.Lorem.Paragraph(),
                Grupo.Nivel,
                Grupo.Justificativa,
                _mensagens
            );

            Grupo = grupoAlterado;

            return this;
        }

        public GrupoDomBuilder DefinirDescricaoInvalida() {
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

        public GrupoDomBuilder DefinirDescricaoNula() {
            var grupoAlterado = new GrupoDom(
                Grupo.Nome,
                null,
                Grupo.Nivel,
                Grupo.Justificativa,
                _mensagens
            );

            Grupo = grupoAlterado;

            return this;
        }

        public GrupoDomBuilder DefinirNivelInferior() {
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

        public GrupoDomBuilder DefinirNivelSuperior() {
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

        public GrupoDomBuilder DefinirJustificativaValida() {
            var grupoAlterado = new GrupoDom(
                Grupo.Nome,
                Grupo.Descricao,
                Grupo.Nivel,
                _faker.Lorem.Paragraph(),
                _mensagens
            );

            Grupo = grupoAlterado;

            return this;
        }

        public GrupoDomBuilder DefinirJustificativaInvalida() {
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

        public GrupoDomBuilder DefinirJustificativaNula() {
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
    }
}