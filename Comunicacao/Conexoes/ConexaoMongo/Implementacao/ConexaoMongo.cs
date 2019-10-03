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

        public IMongoCollection<T> CriarNovaConexao<T>(BancoMongo banco, string colecao)
        {
            return new MongoClient(LeitorArquivos.ObterConnectionString(banco))
                .GetDatabase(banco.ToString())
                .GetCollection<T>(colecao);
        }

        private string ObterConnectionString(BancoMongo colecao)
        {
            return LeitorArquivos.ObterConnectionString(colecao);
        }
    }
}
