using Comunicacao.ConexaoBanco.Interface;
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
            return _conexao.Consultar<IUsuario, string>("Usuario/buscarUsuarioPorNome", nome, Comunicacao.ConexaoBanco.Banco.SHAREDB).FirstOrDefault();
        }
    }
}
