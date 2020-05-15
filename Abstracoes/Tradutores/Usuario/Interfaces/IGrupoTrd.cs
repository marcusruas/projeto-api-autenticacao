using Abstracoes.Representacoes.Usuario.Grupo;
using Dominio.Logica.Usuario;
using MandradePkgs.Mensagens;

namespace Abstracoes.Tradutores.Usuario.Interfaces
{
    public interface IGrupoTrd
    {
        GrupoDom MapearParaDominio(GrupoInclusaoDto grupo, IMensagensApi mensagens);
        GrupoDom MapearParaDominio(GrupoDto grupo, IMensagensApi mensagens);
        GrupoDpo MapearParaDpo(GrupoInclusaoDto grupo);
        GrupoDto MapearParaDto(GrupoDpo grupo);
    }
}