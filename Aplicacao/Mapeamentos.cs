using Aplicacao.Grupo;
using Aplicacao.Pessoa;
using AutoMapper;
using Dominio.Grupo;
using Dominio.Pessoa;
using System.ComponentModel;
using System.Reflection;
using static MandradePkgs.Conexoes.Mapeamentos.DboSqlMapper;

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
                .ForMember(dbo => dbo.Id, opt => opt.Ignore())
                .ForMember(dbo => dbo.Nivel, opt => opt.MapFrom(map => (int)map.Nivel))
                .ReverseMap();

            MapearRetornoObjeto<GrupoDbo>();
            #endregion

            #region Pessoas
            cnf.CreateMap<PessoaDom, PessoaDbo>()
                .ForMember(dbo => dbo.Id, opt => opt.Ignore())
                .ReverseMap();

            MapearRetornoObjeto<PessoaDbo>();
            #endregion
        }

        private static void MapeamentosDominioDto(this IMapperConfigurationExpression cnf) {
            #region Grupos
            cnf.CreateMap<GrupoDom, GrupoDto>()
                .ReverseMap();

            cnf.CreateMap<GrupoDbo, GrupoDto>()
                .ForMember(dto => (int)dto.Nivel, opt => opt.MapFrom(map => map.Nivel))
                .ReverseMap();

            cnf.CreateMap<NivelGrupo, NivelGrupoDto>()
                .ForMember(dto => dto.Nivel, opt => opt.MapFrom(map => (int)map))
                .ForMember(dto => dto.Descricao, opt => opt.MapFrom(map => map.ToString()));
            #endregion

            #region Pessoas
            cnf.CreateMap<PessoaDom, PessoaDto>()
                .ForMember(dto => dto.Id, opt => opt.Ignore())
                .ReverseMap();

            cnf.CreateMap<PessoaDto, PessoaDbo>()
                .ReverseMap();
            #endregion
        }
    }
}
