using AutoMapper;
using Dominio.Logica.Grupo;
using Dominio.Logica.Pessoa;
using Dominio.Representacao.Grupo;
using Dominio.Representacao.Pessoa;
using static MandradePkgs.Conexoes.Mapeamentos.DpoSqlMapper;

namespace Dominio.Representacoes
{
    public static class Mapeamentos
    {
        public static MapperConfiguration DefinirConfiguracoesMapeamento()
        {
            return new MapperConfiguration(cnf =>
            {
                cnf.MapeamentosDominiodpo();
                cnf.MapeamentosDominioDto();
            });
        }

        private static void MapeamentosDominiodpo(this IMapperConfigurationExpression cnf)
        {
            #region Grupos
            cnf.CreateMap<GrupoDom, GrupoDpo>()
                .ForMember(dpo => dpo.Id, opt => opt.Ignore())
                .ForMember(dpo => dpo.Nivel, opt => opt.MapFrom(map => (int)map.Nivel))
                .ReverseMap();

            MapearRetornoObjeto<GrupoDpo>();
            #endregion

            #region Pessoas
            cnf.CreateMap<PessoaDom, PessoaDpo>()
                .ForMember(dpo => dpo.Id, opt => opt.Ignore())
                .ForMember(dpo => dpo.Cpf, opt => opt.MapFrom(v => v.Cpf.ValorNumerico))
                .ForMember(dpo => dpo.Telefone, opt => opt.MapFrom(v => v.Telefone.ValorNumerico))
                .ReverseMap();

            MapearRetornoObjeto<PessoaDpo>();
            #endregion
        }

        private static void MapeamentosDominioDto(this IMapperConfigurationExpression cnf)
        {
            #region Grupos
            cnf.CreateMap<GrupoDom, GrupoDto>()
                .ReverseMap();

            cnf.CreateMap<GrupoDpo, GrupoDto>()
                .ForMember(dto => (int)dto.Nivel.Nivel, opt => opt.MapFrom(map => map.Nivel))
                .ReverseMap();

            cnf.CreateMap<NivelGrupo, NivelGrupoDto>()
                .ForMember(dto => dto.Nivel, opt => opt.MapFrom(map => (int)map))
                .ForMember(dto => dto.Descricao, opt => opt.MapFrom(map => map.ToString()));
            #endregion

            #region Pessoas
            cnf.CreateMap<PessoaDom, PessoaDto>()
                .ForMember(dto => dto.Id, opt => opt.Ignore())
                .ForMember(dto => dto.Cpf, opt => opt.MapFrom(v => v.Cpf.ValorNumerico))
                .ForMember(dto => dto.Telefone,
                           opt => opt.MapFrom(v => v.Telefone.ValorNumerico.ToString()))
                .ReverseMap();

            cnf.CreateMap<PessoaDto, PessoaDpo>()
                .ForMember(dpo => dpo.Cpf, opt => opt.MapFrom(v => v.Cpf.ToString()))
                .ForMember(dto => dto.Telefone, opt => opt.MapFrom(v => v.Telefone))
                .ReverseMap();
            #endregion
        }
    }
}
