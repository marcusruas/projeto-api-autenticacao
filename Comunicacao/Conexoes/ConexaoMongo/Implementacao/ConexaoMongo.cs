using Comunicacao.Conexoes.ConexaoMongo.Interface;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Comunicacao.Conexoes.ConexaoMongo.Implementacao
{
    public class ConexaoMongo : IConexaoMongo
    {
        public string ObterConsultaArquivoJSON(string nomeArquivo)
        {
            return LeitorArquivos.LerArquivoJSON(nomeArquivo);
        }

        public MongoClient CriarNovaConexao(Colecao colecao)
        {
            return new MongoClient(LeitorArquivos.ObterConnectionString(colecao));
        }

        public (string, MongoClient) ObterComandoJSONParaBanco(string nomeArquivo, Colecao colecao)
        {
            return (
                ObterConsultaArquivoJSON(nomeArquivo),
                CriarNovaConexao(colecao)
            );
        }

        private string ObterConnectionString(Colecao colecao)
        {
            return LeitorArquivos.ObterConnectionString(colecao);
        }
    }
}
