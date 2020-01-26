using Aplicacao.Grupo;
using Dominio.Grupo;
using System.Collections.Generic;

namespace Servico.Grupo.Interface
{
    public interface IGrupoSrv
    {
        bool InserirNovoUsuario(GrupoDto grupo);
        List<GrupoDto> GruposPorNivel(int nivel);
        bool AtualizarNivelGrupo(string grupo, int nivel, string justificativa);
        bool ExcluirGrupo(string grupo);
    }
}
