using Dominio.Usuario.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorio.Usuario.Interface
{
    public interface IUsuarioRep
    {
        IUsuario BuscarUsuarioPorNome(string nome);
    }
}
