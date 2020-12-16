using Dapper;
using MandradePkgs.Conexoes;
using static MandradePkgs.Conexoes.Mapeamentos.DpoSqlMapper;
using Repositorios.Usuario.Interfaces;
using Abstracoes.Representacoes.Usuario.Pessoa;
using System.Collections.Generic;
using System.Linq;

namespace Repositorios.Usuario.Implementacoes
{
    public class PessoaRep : IPessoaRep
    {
        private IConexaoSQL _conexao { get; }

        public PessoaRep(IConexaoSQL conexao)
        {
            _conexao = conexao;
        }

        public bool InserirPessoa(PessoaDpo pessoa)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "insertPessoa", "SHAREDB");
            var parametros = DpoParaParametros(pessoa, new { pessoa.Id });
            return conexao.Execute(comando, parametros) == 1;
        }

        public List<PessoaDpo> BuscarPessoas(FiltroBuscaPessoasDto filtro)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "selectPessoas", "SHAREDB");

            var builder = new SqlBuilder();
            var selector = builder.AddTemplate(comando);

            if (filtro.PossuiNome())
                builder.Where("NOME LIKE @NOME", new { Nome = $"%{filtro.nome}%" });

            if (filtro.PossuiCpf())
                builder.Where("CPF = @CPF", new { Cpf = filtro.cpf });

            return conexao.Query<PessoaDpo>(selector.RawSql, selector.Parameters).ToList();
        }

        public bool UpdateDadosPessoa(PessoaDpo pessoa)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "updatePessoa", "SHAREDB");
            var parametros = DpoParaParametros(pessoa);
            return conexao.Execute(comando, parametros) == 1;
        }

        public bool DeletarPessoa(int id)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "deletePessoa", "SHAREDB");
            var parametros = DpoParaParametros(new { id });
            return conexao.Execute(comando, parametros) == 1;
        }

        public PessoaDpo ObterPessoaPorId(int id)
        {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "selectPessoaPorId", "SHAREDB");
            var parametros = DpoParaParametros(new { Id = id });
            return conexao.Query<PessoaDpo>(comando, parametros).FirstOrDefault();
        }
    }
}
