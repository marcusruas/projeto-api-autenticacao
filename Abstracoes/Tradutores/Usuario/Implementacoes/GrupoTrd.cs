using Abstracoes.Representacoes.Usuario.Grupo;
using Abstracoes.Tradutores.Usuario.Interfaces;
using Dominio.Logica.Usuario;
using MandradePkgs.Mensagens;
using SharedKernel.ObjetosValor.Enum;

namespace Abstracoes.Tradutores.Usuario.Implementacoes
{
    public class GrupoTrd : IGrupoTrd
    {
        public GrupoDom MapearParaDominio(GrupoDto grupo, IMensagensApi mensagens) =>
            new GrupoDom(
                grupo.Id,
                grupo.Nome,
                grupo.Descricao,
                grupo.Nivel,
                grupo.Justificativa,
                mensagens
            );

        public GrupoDom MapearParaDominio(GrupoDpo grupo, IMensagensApi mensagens) =>
            new GrupoDom(
                grupo.Id,
                grupo.Nome,
                grupo.Descricao,
                (NivelGrupo)grupo.Nivel,
                grupo.Justificativa,
                mensagens
            );

        public GrupoDpo MapearParaDpo(GrupoDto grupo) =>
            new GrupoDpo()
            {
                Id = grupo.Id,
                Nome = grupo.Nome,
                Descricao = grupo.Descricao,
                Justificativa = grupo.Justificativa,
                Nivel = (int)grupo.Nivel
            };

        public GrupoDto MapearParaDto(GrupoDom grupo) =>
            new GrupoDto()
            {
                Id = grupo.Id,
                Nome = grupo.Nome,
                Descricao = grupo.Descricao,
                Nivel = grupo.Nivel,
                Justificativa = grupo.Justificativa
            };

        public GrupoDto MapearParaDto(GrupoDpo grupo) =>
            new GrupoDto()
            {
                Id = grupo.Id,
                Nome = grupo.Nome,
                Descricao = grupo.Descricao,
                Nivel = (NivelGrupo)grupo.Nivel,
                Justificativa = grupo.Justificativa
            };
    }
}