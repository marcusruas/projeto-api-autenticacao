using System.Collections.Generic;
using Dominio.Representacoes.Grupo;

namespace Repositorio.Grupo.Interface
{
    public interface IGrupoRep
    {
        bool AdicionarGrupo(GrupoDpo grupo);
        List<GrupoDpo> ObterGruposPorNivel(int nivel);
        GrupoDpo ObterDadosGrupo(string grupo);
        bool AtualizarNivelGrupo(string grupo, int nivel, string justificativa);
        bool DeletarGrupo(string grupo);
    }
}
