using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Comunicacao.ConexaoBanco.Interface
{
   public interface IConexaoBanco
    {
        string ObterConsultaArquivoSQL(string nomeArquivo);
        IDbConnection CriarNovaConexao();
        List<T> Consultar<T>(string consulta);
        List<T> Consultar<T>(string consulta, T parametros);
        int Executar(string consulta);
        int Executar<T>(string consulta, T parametros);
    }
}
