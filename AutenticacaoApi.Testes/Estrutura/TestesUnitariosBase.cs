using MandradePkgs.Mensagens;
using MandradePkgs.Mensagens.Estrutura.Implementacao;
using Moq;
using Xunit.Abstractions;

namespace AutenticacaoApi.Testes
{
    public abstract class TestesUnitariosBase
    {
        protected IMensagensApi _mensagens { get; }
        protected TestLogger _logs { get; }

        public TestesUnitariosBase(ITestOutputHelper output) {
            _mensagens = Mock.Of<MensagensApi>();
            _logs = new TestLogger(_mensagens, output);
        }
    }
}