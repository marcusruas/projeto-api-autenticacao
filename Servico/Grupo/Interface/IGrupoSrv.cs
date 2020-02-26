using Dominio.Representacoes.Grupo;
using System.Collections.Generic;

namespace Servico.Grupo.Interface
{
    public interface IGrupoSrv
    {
        bool InserirNovoUsuario(GrupoDto grupo);
        List<GrupoDto> GruposPorNivel(int nivel);
        GrupoDto ObterDadosGrupo(string grupo);
        bool AtualizarNivelGrupo(string grupo, int nivel, string justificativa);
        bool ExcluirGrupo(string grupo);
    }
}
