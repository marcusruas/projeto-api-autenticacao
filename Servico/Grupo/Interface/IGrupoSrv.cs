using Dominio.Grupo;

namespace Servico.Grupo.Interface
{
    public interface IGrupoSrv
    {
        bool InserirNovoUsuario(string nome, string descricao, NivelGrupo nivel);
    }
}
