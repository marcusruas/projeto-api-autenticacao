using System.Collections.Generic;
using Abstracoes.Representacoes.Usuario.Grupo;

namespace Repositorios.Usuario.Interfaces
{
    public interface IGrupoRep
    {
        bool AdicionarGrupo(GrupoDpo grupo);
        List<GrupoDpo> ObterGruposPorNivel(int nivel);
        GrupoDpo ObterDadosGrupo(string grupo);
        GrupoDpo ObterGrupoPorId(int id);
        bool AtualizarNivelGrupo(string grupo, int nivel, string justificativa);
        bool DeletarGrupo(int id);
    }
}
