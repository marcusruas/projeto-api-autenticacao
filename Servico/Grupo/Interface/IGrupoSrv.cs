using Dominio.Grupo;
using System.Collections.Generic;

namespace Servico.Grupo.Interface
{
    public interface IGrupoSrv
    {
        bool InserirNovoUsuario(string nome, string descricao, NivelGrupo nivel);
        List<GrupoDom> GruposPorNivel(int nivel);
        bool AtualizarNivelGrupo(string grupo, int nivel);
        bool ExcluirGrupo(string grupo);
    }
}
