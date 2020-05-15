using Abstracoes.Representacoes.Usuario.Grupo;

namespace Servicos.Usuario.Interfaces
{
    public interface IGrupoSrv
    {
        bool InserirNovoUsuario(GrupoInclusaoDto grupo);
        bool ExcluirGrupo(int id);
        GrupoDto PesquisarGrupoPorId(int id);
    }
}
