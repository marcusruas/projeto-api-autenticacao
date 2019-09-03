using Comunicacao.ConexaoBanco.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using System.Text;
using System.Data.SqlClient;
using Dapper;
using System.Linq;

namespace Comunicacao.ConexaoBanco.Implementacao
{
    class ConexaoBanco : IConexaoBanco
    {
        public string ObterConsultaArquivoSQL(string nomeArquivo)
        {
            return LeitorArquivos.LerArquivoSQL(nomeArquivo);
        }

        public IDbConnection CriarNovaConexao(Banco banco)
        {
            return new SqlConnection(LeitorArquivos.ObterConnectionString(banco));
        }

        public (string, IDbConnection) ObterComandoSQLParaBanco(string nomeArquivo, Banco banco)
        {
            return (
                ObterConsultaArquivoSQL(nomeArquivo),
                CriarNovaConexao(banco)
            );
        }

        public List<T> Consultar<T>(string nomeArquivo, Banco banco)
        {
            try
            {
                var (consulta, conexao) = ObterComandoSQLParaBanco(nomeArquivo, banco);
                return conexao.Query<T>(consulta).ToList();
            }
            catch(SqlException)
            {
                throw new Exception("Ocorreu um erro ao se conectar ao banco de dados");
            }
            catch(Exception)
            {
                throw;
            }
        }

        public List<T> Consultar<T>(string nomeArquivo, T parametros, Banco banco)
        {
            try
            {
                var (consulta, conexao) = ObterComandoSQLParaBanco(nomeArquivo, banco);
                return conexao.Query<T>(consulta, parametros).ToList();
            }
            catch (SqlException)
            {
                throw new Exception("Ocorreu um erro ao se conectar ao banco de dados");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Executar(string nomeArquivo, Banco banco)
        {
            try
            {
                var (consulta, conexao) = ObterComandoSQLParaBanco(nomeArquivo, banco);
                return conexao.Execute(consulta);
            }
            catch (SqlException)
            {
                throw new Exception("Ocorreu um erro ao se conectar ao banco de dados");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Executar<T>(string nomeArquivo, T parametros, Banco banco)
        {
            try
            {
                var (consulta, conexao) = ObterComandoSQLParaBanco(nomeArquivo, banco);
                return conexao.Execute(consulta, parametros);
            }
            catch (SqlException)
            {
                throw new Exception("Ocorreu um erro ao se conectar ao banco de dados");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string ObterConnectionString(Banco banco)
        {
            return LeitorArquivos.ObterConnectionString(banco);
        }
    }
}
