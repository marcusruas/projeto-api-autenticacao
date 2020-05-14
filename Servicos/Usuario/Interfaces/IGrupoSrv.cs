using Abstracoes.Representacoes.Usuario.Grupo;

namespace Servicos.Usuario.Interfaces
{
    public interface IGrupoSrv
    {
        bool InserirNovoUsuario(GrupoDto grupo);
        GrupoDto PesquisarGrupoPorId(int id);
        bool ExcluirGrupo(int id);
    }
}
