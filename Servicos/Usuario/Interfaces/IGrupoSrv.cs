using System.Collections.Generic;
using Abstracoes.Representacoes.Usuario.Grupo;

namespace Servicos.Usuario.Interfaces
{
    public interface IGrupoSrv
    {
        bool InserirNovoUsuario(GrupoInclusaoDto grupo);
        bool ExcluirGrupo(int id);
        bool VincularGrupos(int grupoPai, int grupoFilho);
        GrupoDto PesquisarGrupoPorId(int id);
        GrupoDto ObterPai(int id);
        List<GrupoDto> ListarFilhos(int id);
        List<GrupoDto> ListarTodosGrupos(GrupoPesquisaDto filtro);
    }
}
