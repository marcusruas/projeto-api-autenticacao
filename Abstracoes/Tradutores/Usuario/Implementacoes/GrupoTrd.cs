using Abstracoes.Representacoes.Usuario.Grupo;
using Abstracoes.Tradutores.Usuario.Interfaces;
using Dominio.Logica.Usuario;
using MandradePkgs.Mensagens;

namespace Abstracoes.Tradutores.Usuario.Implementacoes
{
    public class GrupoTrd : IGrupoTrd
    {
        public GrupoDom MapearParaDominio(GrupoDto grupo, IMensagensApi mensagens) =>
            new GrupoDom(
                grupo.Id,
                grupo.Nome,
                grupo.Descricao,
                mensagens
            );

        public GrupoDom MapearParaDominio(GrupoDpo grupo, IMensagensApi mensagens) =>
            new GrupoDom(
                grupo.Id,
                grupo.Nome,
                grupo.Descricao,
                mensagens
            );

        public GrupoDpo MapearParaDpo(GrupoDto grupo) =>
            new GrupoDpo()
            {
                Id = grupo.Id,
                Nome = grupo.Nome,
                Descricao = grupo.Descricao,
            };

        public GrupoDto MapearParaDto(GrupoDom grupo) =>
            new GrupoDto()
            {
                Id = grupo.Id,
                Nome = grupo.Nome,
                Descricao = grupo.Descricao,
            };

        public GrupoDto MapearParaDto(GrupoDpo grupo) =>
            new GrupoDto()
            {
                Id = grupo.Id,
                Nome = grupo.Nome,
                Descricao = grupo.Descricao,
            };
    }
}