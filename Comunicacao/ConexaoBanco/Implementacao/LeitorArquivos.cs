using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Comunicacao.ConexaoBanco.Implementacao
{
    class LeitorArquivos
    {
        private static string DirBuildComunicacao = Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName;

        public static string LerArquivoSQL(string nomeArquivo)
        {
            string pathApi = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            string conteudoArquivo = string.Empty;

            try
            {
                string[] projetoArquivo = nomeArquivo.Split('/');
                string pathArquivo = Path.Combine(pathApi, projetoArquivo[0], "SQL", $"{projetoArquivo[1]}.sql");
                pathArquivo = pathArquivo.Replace("file:\\", "");

                string[] linhas;

                if (!File.Exists(pathArquivo))
                    throw new Exception("Não foi possível localizar o arquivo de consulta ao banco com este nome.");

                linhas = File.ReadAllLines(pathArquivo);
                foreach (string linha in linhas)
                    conteudoArquivo += (linha + " ");
            }
            catch (ArgumentNullException)
            {
                throw new Exception("O arquivo de consulta ao banco de dados está vazio.");
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro ao ler o arquivo SQL indicado.");
            }
            return conteudoArquivo;
        }

        public static string ObterConnectionString(Banco banco)
        {
            string arquivoConexao = Path.Combine(DirBuildComunicacao, "ConexaoBanco" , "conexoes.json");
            List<ConexaoModel> conexoes;
            try
            {
                using (StreamReader r = new StreamReader(arquivoConexao))
                {
                    var json = r.ReadToEnd();
                    conexoes = JsonConvert.DeserializeObject<ConexaoModel[]>(json).ToList();
                }
                foreach (var con in conexoes)
                    if (con.Nome == banco.ToString())
                        return con.ConnectionString;
                return null;
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível localizar a conexão da base desejada.");
            }
        }
    }
}
