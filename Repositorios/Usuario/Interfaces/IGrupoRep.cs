using System.Collections.Generic;
using Abstracoes.Representacoes.Usuario.Grupo;

namespace Repositorios.Usuario.Interfaces
{
    public interface IGrupoRep
    {
        bool AdicionarGrupo(GrupoDpo grupo);
        List<GrupoDpo> ObterGruposPorNivel(int nivel);
        GrupoDpo ObterGrupoPorId(int id);
        bool AtualizarNivelGrupo(GrupoAtualizacaoDto atualizacao);
        bool DeletarGrupo(int id);
    }
}
