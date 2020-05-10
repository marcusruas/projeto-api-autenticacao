using Abstracoes.Representacoes.Usuario.Grupo;
using Dominio.Logica.Usuario;
using MandradePkgs.Mensagens;

namespace Abstracoes.Tradutores.Usuario.Interfaces
{
    public interface IGrupoTrd
    {
        GrupoDto MapearParaDto(GrupoDom grupo);
        GrupoDto MapearParaDto(GrupoDpo grupo);
        GrupoDto MapearParaDto(GrupoAtualizacaoDto atualizacao);
        GrupoDpo MapearParaDpo(GrupoDto grupo);
        GrupoDom MapearParaDominio(GrupoDto grupo, IMensagensApi mensagens);
        GrupoDom MapearParaDominio(GrupoDpo grupo, IMensagensApi mensagens);
    }
}