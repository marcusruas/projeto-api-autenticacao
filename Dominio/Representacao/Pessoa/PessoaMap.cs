using AutoMapper;
using Dominio.Logica.Pessoa;
using static MandradePkgs.Conexoes.Mapeamentos.DpoSqlMapper;

namespace Dominio.Representacao.Pessoa
{
    public static class PessoaMap
    {
        public static void ConfigurarMapeamentosPessoa(this IMapperConfigurationExpression configuracao)
        {
            DefinirMapeamentosRepresentacoes(configuracao);
            MapearRetornoObjeto<PessoaDpo>();
        }

        private static void DefinirMapeamentosRepresentacoes(this IMapperConfigurationExpression cnf)
        {
            cnf.CreateMap<PessoaDom, PessoaDpo>()
                .ForMember(dpo => dpo.Id, opt => opt.Ignore())
                .ForMember(dpo => dpo.Cpf, opt => opt.MapFrom(v => v.Cpf.ValorNumerico))
                .ForMember(dpo => dpo.Telefone, opt => opt.MapFrom(v => v.Telefone.ValorNumerico))
                .ReverseMap();

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
        }
    }
}