using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Comunicacao.Conexoes.ConexaoSQL.Interface
{
    public interface IConexaoSQL
    {
        string ObterConsultaArquivoSQL(string nomeArquivo);
        IDbConnection CriarNovaConexao(BancoSQL banco);
        (string, IDbConnection) ObterComandoSQLParaBanco(string nomeArquivo, BancoSQL banco);
    }
}
