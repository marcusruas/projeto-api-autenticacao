using Dapper;
using MandradePkgs.Conexoes;
using static MandradePkgs.Conexoes.Estrutura.Mapeamento.DpoSqlMapper;
using Infraestrutura.Repositorio.Usuario.Interface;
using System.Collections.Generic;
using System.Linq;
using Infraestrutura.Repositorio.Usuario.Entidade;
using MandradePkgs.Conexoes.Estrutura.Model;

namespace Infraestrutura.Repositorio.Usuario.Implementacao
{
    public class PessoaRp : ClasseRepositorio, IPessoaRp
    {
        private IConexaoSQL _conexao { get; }

        public PessoaRp(IConexaoSQL conexao) : base("Usuario")
        {
            _conexao = conexao;
        }

        public bool InserirPessoa(PessoaDpo pessoa)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "insertPessoa", "SHAREDB");
            var parametros = DpoParaParametros(pessoa, new { pessoa.Id });
            return conexao.Execute(comando, parametros) == 1;
        }

        public List<PessoaDpo> BuscarPessoas(string nome, string cpf)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "selectPessoas", "SHAREDB");

            var builder = new SqlBuilder();
            var selector = builder.AddTemplate(comando);

            if (!string.IsNullOrWhiteSpace(nome))
                builder.Where("NOME LIKE @NOME", new { Nome = $"%{nome}%" });

            if (!string.IsNullOrWhiteSpace(cpf))
                builder.Where("CPF = @CPF", new { Cpf = cpf });

            return conexao.Query<PessoaDpo>(selector.RawSql, selector.Parameters).ToList();
        }

        public bool UpdateDadosPessoa(PessoaDpo pessoa)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "updatePessoa", "SHAREDB");
            var parametros = DpoParaParametros(pessoa);
            return conexao.Execute(comando, parametros) == 1;
        }

        public bool DeletarPessoa(int id)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "deletePessoa", "SHAREDB");
            var parametros = DpoParaParametros(new { id });
            return conexao.Execute(comando, parametros) == 1;
        }

        public PessoaDpo ObterPessoaPorId(int id)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(this, "selectPessoaPorId", "SHAREDB");
            var parametros = DpoParaParametros(new { Id = id });
            return conexao.Query<PessoaDpo>(comando, parametros).FirstOrDefault();
        }
    }
}
