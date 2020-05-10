using System.Collections.Generic;
using Abstracoes.Representacoes.Usuario.Grupo;
using SharedKernel.ObjetosValor.Enum;

namespace Servicos.Usuario.Interfaces
{
    public interface IGrupoSrv
    {
        bool InserirNovoUsuario(GrupoDto grupo);
        List<GrupoDto> GruposPorNivel(NivelGrupo nivel);
        GrupoDto PesquisarGrupoPorId(int id);
        bool AtualizarNivelGrupo(GrupoAtualizacaoDto atualizacao);
        bool ExcluirGrupo(int id);
    }
}
