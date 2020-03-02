using System.Collections.Generic;
using Dominio.ObjetosValor.Enum;
using Dominio.Representacao.Usuario.Grupo;

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
