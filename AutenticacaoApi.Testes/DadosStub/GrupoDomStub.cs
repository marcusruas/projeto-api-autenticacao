using Bogus;

namespace AutenticacaoApi.Testes.DadosStub
{
    public class GrupoDomStub
    {
        private Faker _faker { get; }
        public GrupoDomStub()
        {
            _faker = new Faker();
        }
        public string GerarNomeInvalido() =>
            _faker.Name.FullName().Substring(0, 5);

        public string GerarNomeNumeros() =>
            _faker.Name.FullName().Insert(0, _faker.Random.Int(1, 5).ToString());

        public string GerarNomeValido() =>
            _faker.Name.FullName();

        public string GerarJustificativaDescricaoValida() =>
            _faker.Lorem.Paragraph();

        public string GerarJustificativaDescricaoInvalida() =>
            _faker.Random.String(10);
    }
}