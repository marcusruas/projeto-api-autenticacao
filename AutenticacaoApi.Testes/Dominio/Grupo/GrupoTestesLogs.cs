using AutenticacaoApi.Testes.Builders;
using MandradePkgs.Mensagens;
using Xunit.Abstractions;

namespace AutenticacaoApi.Testes.Dominio.Grupo
{
    public class GrupoTestesLogs
    {
        private IMensagensApi _mensagens;
        private ITestOutputHelper _output;
        private GrupoDomBuilder _builder;
        public string Teste { get; private set; }

        public GrupoTestesLogs(IMensagensApi mensagens, ITestOutputHelper output, GrupoDomBuilder builder)
        {
            _mensagens = mensagens;
            _output = output;
            _builder = builder;
        }

        public void DefinirTeste(string teste) => Teste = teste;

        public void GravarLogs(){
            _output.WriteLine(Teste);
            _output.WriteLine(GerarLogObjeto());
            _output.WriteLine(GerarLogMensagens());
        }

        private string GerarLogObjeto(){
            var grupo = _builder.Build();

            return $@"Dados do Objeto: \n
                Nome: {grupo.Nome},
                Nivel: {grupo.Nivel},
                Descricao: {grupo.Descricao},
                Justificativa: {grupo.Justificativa}
            ";
        }

        private string GerarLogMensagens(){
            var stringCompleta = "Mensagens: \n";

            foreach(var msg in _mensagens.Mensagens)
                stringCompleta += "\t"+msg.Texto+"\n";

            return stringCompleta;
        }
    }
}