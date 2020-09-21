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
                grupo.Pai.HasValue ? grupo.Pai.Value : 0,
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
    }
}