using System.Collections.Generic;
using Abstracoes.Representacoes.Usuario.Grupo;
using SharedKernel.ObjetosValor.Enum;

namespace Servicos.Usuario.Interfaces
{
    public interface IGrupoSrv
    {
        bool InserirNovoUsuario(GrupoDto grupo);
        GrupoDto PesquisarGrupoPorId(int id);
        bool ExcluirGrupo(int id);
    }
}
