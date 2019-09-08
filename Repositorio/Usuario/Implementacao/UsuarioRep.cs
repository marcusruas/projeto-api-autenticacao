using Comunicacao.ConexaoBanco.Interface;
using Dapper;
using Dominio.Usuario.Interface;
using Repositorio.Usuario.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositorio.Usuario.Implementacao
{
    public class UsuarioRep : IUsuarioRep
    {
        private IConexaoBanco _conexao;
        public UsuarioRep(IConexaoBanco conexao)
        {
            _conexao = conexao;
        }

        public IUsuario BuscarUsuarioPorNome(string nome)
        {
            var (script, conexao) = _conexao.ObterComandoSQLParaBanco("Usuario/buscarUsuarioPorNome", Comunicacao.ConexaoBanco.Banco.SHAREDB);
            return conexao.Query<IUsuario>(script, new { nome }).FirstOrDefault();
        }
    }
}
