using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comunicacao.Conexoes.ConexaoMongo.Interface
{
    public interface IConexaoMongo
    {
        IMongoCollection<T> CriarNovaConexao<T>(BancoMongo banco, string colecao);
    }
}
