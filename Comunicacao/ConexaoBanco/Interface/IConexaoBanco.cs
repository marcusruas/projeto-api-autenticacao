using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Comunicacao.ConexaoBanco.Interface
{
   public interface IConexaoBanco
    {
        string ObterConsultaArquivoSQL(string nomeArquivo, Banco banco);
        IDbConnection CriarNovaConexao(Banco banco);
        List<T> Consultar<T>(string consulta, Banco banco);
        List<T> Consultar<T>(string consulta, T parametros, Banco banco);
        int Executar(string consulta, Banco banco);
        int Executar<T>(string consulta, T parametros, Banco banco);
    }
}
