﻿using Aplicacao.Pessoa;
using Dapper;
using MandradePkgs.Conexoes;
using static MandradePkgs.Conexoes.Mapeamentos.DboSqlMapper;
using Repositorio.Pessoa.Interface;
using System.Data;

namespace Repositorio.Pessoa.Implementacao
{
    public class PessoaRep : IPessoaRep
    {
        private IConexaoSQL _conexao { get; }

        public PessoaRep(IConexaoSQL conexao) {
            _conexao = conexao;
        }

        public bool InserirPessoa(PessoaDbo pessoa) {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "insertPessoa", "SHAREDB");
            var parametros = DboParaParametros(pessoa, new { pessoa.Id });
            return conexao.Execute(comando, parametros) == 1;
        }

        public PessoaDbo BuscarPessoaCpf(long cpf) {
            var (comando, conexao) = _conexao.ObterComandoSQLParaBanco(GetType(), "selectPessoaCpf", "SHAREDB");
            var parametros = new DynamicParameters();
            parametros.Add("cpf", cpf);
            return conexao.QueryFirstOrDefault<PessoaDbo>(comando, parametros);
        }
    }
}
