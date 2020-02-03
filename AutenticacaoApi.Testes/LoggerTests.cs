using System.Linq;
using Xunit.Abstractions;

namespace AutenticacaoApi.Testes
{
    public class LoggerTests<T>
    {
        public ITestOutputHelper _output { get; }

        public LoggerTests(ITestOutputHelper output)
        {
            _output = output;
        }
    }
}