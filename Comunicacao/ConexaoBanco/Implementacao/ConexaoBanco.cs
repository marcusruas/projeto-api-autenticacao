using Comunicacao.ConexaoBanco.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Comunicacao.ConexaoBanco.Implementacao
{
    public class ConexaoBanco : IConexaoBanco
    {
        public List<T> Consultar<T>(string consulta)
        {
            throw new NotImplementedException();
        }

        public List<T> Consultar<T>(string consulta, T parametros)
        {
            throw new NotImplementedException();
        }

        public IDbConnection CriarNovaConexao()
        {
            throw new NotImplementedException();
        }

        public int Executar(string consulta)
        {
            throw new NotImplementedException();
        }

        public int Executar<T>(string consulta, T parametros)
        {
            throw new NotImplementedException();
        }

        public string ObterConsultaArquivoSQL(string nomeArquivo)
        {
            throw new NotImplementedException();
        }
    }
}
