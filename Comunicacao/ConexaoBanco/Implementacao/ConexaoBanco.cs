using Comunicacao.ConexaoBanco.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using System.Text;

namespace Comunicacao.ConexaoBanco.Implementacao
{
    public class ConexaoBanco : IConexaoBanco
    {
        public List<T> Consultar<T>(string consulta, Banco banco)
        {
            throw new NotImplementedException();
        }

        public List<T> Consultar<T>(string consulta, T parametros, Banco banco)
        {
            throw new NotImplementedException();
        }

        public IDbConnection CriarNovaConexao(Banco banco)
        {
            throw new NotImplementedException();
        }

        public int Executar(string consulta, Banco banco)
        {
            throw new NotImplementedException();
        }

        public int Executar<T>(string consulta, T parametros, Banco banco)
        {
            throw new NotImplementedException();
        }

        public string ObterConsultaArquivoSQL(string nomeArquivo, Banco banco)
        {
            throw new NotImplementedException();
        }

        private string ObterConnectionString(Banco banco)
        {
            return LeitorArquivos.ObterStringBanco(banco);
        }
    }
}
