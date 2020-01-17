using Aplicacao.Grupo;
using AutoMapper;
using Dominio.Grupo;
using System;

namespace Aplicacao
{
    public static class Mapeamentos
    {
        public static MapperConfiguration DefinirConfiguracoesMapeamento() {
            return new MapperConfiguration(cnf => cnf.MapeamentosDominioDbo());
        }

        private static void MapeamentosDominioDbo(this IMapperConfigurationExpression cnf) {
            #region Grupos
            cnf.CreateMap<GrupoDom, GrupoDbo>()
                .ForMember(dbo => dbo.IdGrupo, opt => opt.Ignore())
                .ForMember(dbo => dbo.Nivel, opt => opt.MapFrom(map => (int)map.Nivel))
                .ReverseMap();
            #endregion
        }
    }
}
