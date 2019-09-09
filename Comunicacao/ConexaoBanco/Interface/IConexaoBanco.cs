using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Comunicacao.ConexaoBanco.Interface
{
    public interface IConexaoBanco
    {
        string ObterConsultaArquivoSQL(string nomeArquivo);
        IDbConnection CriarNovaConexao(Banco banco);
        (string, IDbConnection) ObterComandoSQLParaBanco(string nomeArquivo, Banco banco);
    }
}
