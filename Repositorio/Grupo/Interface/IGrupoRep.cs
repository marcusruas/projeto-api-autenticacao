using Aplicacao.Grupo;
using System.Collections.Generic;

namespace Repositorio.Grupo.Interface
{
    public interface IGrupoRep
    {
        bool AdicionarGrupo(GrupoDpo grupo);
        List<GrupoDpo> ObterGruposPorNivel(int nivel);
        bool AtualizarNivelGrupo(string grupo, int nivel, string justificativa);
        bool DeletarGrupo(string grupo);
    }
}
