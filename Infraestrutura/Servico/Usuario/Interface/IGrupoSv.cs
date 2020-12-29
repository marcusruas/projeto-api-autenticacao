using System.Collections.Generic;
using Infraestrutura.Servico.Usuario.Entidade;

namespace Infraestrutura.Servicos.Usuario.Interface
{
    public interface IGrupoSv
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
