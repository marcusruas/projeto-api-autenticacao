using Dominio.Grupo;
using System.Collections.Generic;

namespace Servico.Grupo.Interface
{
    public interface IGrupoSrv
    {
        bool InserirNovoUsuario(string nome, string descricao, NivelGrupo nivel);
        List<GrupoDom> GruposPorNivel(int nivel);
    }
}
