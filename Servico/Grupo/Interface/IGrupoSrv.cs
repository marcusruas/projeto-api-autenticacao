using System;
using System.Collections.Generic;
using System.Text;

namespace Servico.Grupo.Interface
{
    public interface IGrupoSrv
    {
        bool InserirNovoUsuario(string nome, int nivel);
    }
}
