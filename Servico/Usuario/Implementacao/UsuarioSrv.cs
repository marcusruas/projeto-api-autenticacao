using Dominio.Usuario.Interface;
using Repositorio.Usuario.Interface;
using Servico.Usuario.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servico.Usuario.Implementacao
{
    public class UsuarioSrv : IUsuarioSrv
    {
        private IUsuarioRep _repositorio;
        public UsuarioSrv(IUsuarioRep repositorio)
        {
            _repositorio = repositorio;
        }

        public IUsuario BuscarUsuarioPorNome(string nome)
        {
            return _repositorio.BuscarUsuarioPorNome(nome);
        }
    }
}
