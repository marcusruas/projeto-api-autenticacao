using System.Collections.Generic;
using Abstracao.Representacao.Usuario.Grupo;
using SharedKernel.ObjetosValor.Enum;

namespace Servico.Usuario.Interface
{
    public interface IGrupoSrv
    {
        bool InserirNovoUsuario(GrupoDto grupo);
        List<GrupoDto> GruposPorNivel(NivelGrupo nivel);
        GrupoDto ObterDadosGrupo(string grupo);
        bool AtualizarNivelGrupo(string grupo, NivelGrupo nivel, string justificativa);
        bool ExcluirGrupo(string grupo);
    }
}
