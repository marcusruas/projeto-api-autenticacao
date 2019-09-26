using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comunicacao.Conexoes.ConexaoMongo.Interface
{
    public interface IConexaoMongo
    {
        string ObterConsultaArquivoJSON(string nomeArquivo);
        MongoClient CriarNovaConexao(Colecao colecao);
        (string, MongoClient) ObterComandoJSONParaBanco(string nomeArquivo, Colecao colecao);
    }
}
