using System.Collections.Generic;
using Abstracoes.Representacoes.Usuario.Grupo;

namespace Repositorios.Usuario.Interfaces
{
    public interface IGrupoRep
    {
        bool AdicionarGrupo(GrupoDpo grupo);
        GrupoDpo ObterGrupoPorId(int id);
        bool DeletarGrupo(int id);
    }
}
