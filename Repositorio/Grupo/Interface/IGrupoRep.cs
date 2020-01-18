using Aplicacao.Grupo;
using System.Collections.Generic;

namespace Repositorio.Grupo.Interface
{
    public interface IGrupoRep
    {
        bool AdicionarGrupo(GrupoDbo grupo);
        List<GrupoDbo> ObterGruposPorNivel(int nivel);
    }
}
