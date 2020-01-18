using Aplicacao.Grupo;
using AutoMapper;
using Dominio.Grupo;
using System;

namespace Aplicacao
{
    public static class Mapeamentos
    {
        public static MapperConfiguration DefinirConfiguracoesMapeamento() {
            return new MapperConfiguration(cnf => {
                cnf.MapeamentosDominioDbo();
                cnf.MapeamentosDominioDto();
            });
        }

        private static void MapeamentosDominioDbo(this IMapperConfigurationExpression cnf) {
            #region Grupos
            cnf.CreateMap<GrupoDom, GrupoDbo>()
                .ForMember(dbo => dbo.IdGrupo, opt => opt.Ignore())
                .ForMember(dbo => dbo.Nivel, opt => opt.MapFrom(map => (int)map.Nivel))
                .ReverseMap();
            #endregion
        }

        private static void MapeamentosDominioDto(this IMapperConfigurationExpression cnf) {
            #region Grupos
            cnf.CreateMap<GrupoDom, GrupoDto>().ReverseMap();

            cnf.CreateMap<NivelGrupo, NivelGrupoDto>()
                .ForMember(dto => dto.Nivel, opt => opt.MapFrom(map => (int)map))
                .ForMember(dto => dto.Descricao, opt => opt.MapFrom(map => map.ToString()));
            #endregion
        }
    }
}
