using AutoMapper;
using Dominio.Logica.Usuario;
using static MandradePkgs.Conexoes.Mapeamentos.DpoSqlMapper;

namespace Dominio.Representacao.Usuario.Grupo
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
            cnf.CreateMap<GrupoDom, GrupoDto>()
                .ReverseMap();

            cnf.CreateMap<GrupoDto, GrupoDpo>()
                .ForMember(dpo => dpo.Nivel, opt => opt.MapFrom(map => (int)map.Nivel))
                .ReverseMap();
        }
    }
}