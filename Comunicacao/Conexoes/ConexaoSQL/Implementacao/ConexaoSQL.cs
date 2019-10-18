﻿using System.Data;
using System.Data.SqlClient;
using Comunicacao.Conexoes.ConexaoSQL.Interface;

namespace Comunicacao.Conexoes.ConexaoSQL.Implementacao
{
    public class ConexaoSQL : IConexaoSQL
    {
        public string ObterConsultaArquivoSQL(string nomeArquivo) {
            return LeitorArquivos.LerArquivoSQL(nomeArquivo);
        }

        public IDbConnection CriarNovaConexao(BancoSQL banco) {
            return new SqlConnection(LeitorArquivos.ObterConnectionString(banco));
        }

        public (string, IDbConnection) ObterComandoSQLParaBanco(string nomeArquivo, BancoSQL banco) {
            return (
                ObterConsultaArquivoSQL(nomeArquivo),
                CriarNovaConexao(banco)
            );
        }

        private string ObterConnectionString(BancoSQL banco) {
            return LeitorArquivos.ObterConnectionString(banco);
        }
    }
}
