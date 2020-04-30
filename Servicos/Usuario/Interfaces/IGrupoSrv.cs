using System.Collections.Generic;
using Abstracoes.Representacoes.Usuario.Grupo;
using SharedKernel.ObjetosValor.Enum;

namespace Servicos.Usuario.Interfaces
{
    public interface IGrupoSrv
    {
        bool InserirNovoUsuario(GrupoDto grupo);
        List<GrupoDto> GruposPorNivel(NivelGrupo nivel);
        GrupoDto ObterDadosGrupo(string grupo);
        GrupoDto PesquisarGrupoPorId(int id);
        bool AtualizarNivelGrupo(string grupo, NivelGrupo nivel, string justificativa);
        bool ExcluirGrupo(string grupo);
    }
}
