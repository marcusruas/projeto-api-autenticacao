using AutoMapper;
using Dominio.Logica.Grupo;
using static MandradePkgs.Conexoes.Mapeamentos.DpoSqlMapper;

namespace Dominio.Representacao.Grupo
{
    public static class GrupoMap
    {
        public static void ConfigurarMapeamentosGrupo(this IMapperConfigurationExpression configuracao)
        {
            DefinirMapeamentosRepresentacoes(configuracao);
            MapearRetornoObjeto<GrupoDpo>();
        }

        private static void DefinirMapeamentosRepresentacoes(this IMapperConfigurationExpression cnf)
        {
            cnf.CreateMap<GrupoDom, GrupoDpo>()
                .ForMember(dpo => dpo.Id, opt => opt.Ignore())
                .ForMember(dpo => dpo.Nivel, opt => opt.MapFrom(map => (int)map.Nivel))
                .ReverseMap();

            cnf.CreateMap<GrupoDom, GrupoDto>()
                .ReverseMap();

            cnf.CreateMap<GrupoDpo, GrupoDto>()
                .ForMember(dto => (int)dto.Nivel.Nivel, opt => opt.MapFrom(map => map.Nivel))
                .ReverseMap();

            cnf.CreateMap<NivelGrupo, NivelGrupoDto>()
                .ForMember(dto => dto.Nivel, opt => opt.MapFrom(map => (int)map))
                .ForMember(dto => dto.Descricao, opt => opt.MapFrom(map => map.ToString()));
        }
    }
}