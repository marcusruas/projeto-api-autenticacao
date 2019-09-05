using Dominio.Usuario.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servico.Usuario.Interface
{
    public interface IUsuarioSrv
    {
        IUsuario BuscarUsuarioPorNome(string nome);
    }
}
