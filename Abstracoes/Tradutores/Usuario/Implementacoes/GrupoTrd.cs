using Abstracoes.Representacoes.Usuario.Grupo;
using Abstracoes.Tradutores.Usuario.Interfaces;
using Dominio.Logica.Usuario;
using MandradePkgs.Mensagens;

namespace Abstracoes.Tradutores.Usuario.Implementacoes
{
    public class GrupoTrd : IGrupoTrd
    {
        public GrupoDom MapearParaDominio(GrupoInclusaoDto grupo, IMensagensApi mensagens) =>
            new GrupoDom(
                grupo.IdPai.HasValue ? grupo.IdPai.Value : 0,
                grupo.Nome,
                grupo.Descricao,
                mensagens
            );

        public GrupoDom MapearParaDominio(GrupoDto grupo, IMensagensApi mensagens) =>
            new GrupoDom(
                grupo.Id,
                grupo.Nome,
                grupo.Descricao,
                grupo.Pai,
                mensagens
            );

        public GrupoDpo MapearParaDpo(GrupoInclusaoDto grupo) =>
            new GrupoDpo(
                0,
                grupo.Nome,
                grupo.Descricao,
                grupo.IdPai.HasValue ? grupo.IdPai.Value : 0
            );

        public GrupoDto MapearParaDto(GrupoDpo grupo) =>
            new GrupoDto(
                grupo.Id,
                grupo.Nome,
                grupo.Descricao,
                grupo.Pai
            );
    }
}