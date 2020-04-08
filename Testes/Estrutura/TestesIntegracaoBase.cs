using Dapper;
using MandradePkgs.Conexoes;
using MandradePkgs.Conexoes.Estrutura.Implementacoes;
using MandradePkgs.Mensagens;
using MandradePkgs.Mensagens.Estrutura.Implementacoes;
using Moq;
using Xunit.Abstractions;

namespace Testes
{
    public class TestesIntegracaoBase
    {
        protected IMensagensApi _mensagens { get; }
        protected TesteLogs _logs { get; }
        protected IConexaoSQL _conexao { get; }

        public TestesIntegracaoBase(ITestOutputHelper output)
        {
            _mensagens = Mock.Of<MensagensApi>();
            _logs = new TesteLogs(_mensagens, output);
            _conexao = new ConexaoSQL(this.GetType());
        }

        public void ExecutarScript(string script, string nomeBanco)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), script, nomeBanco);
            conexao.Execute(comando);
        }
    }
}