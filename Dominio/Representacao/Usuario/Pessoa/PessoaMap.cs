using AutoMapper;
using Dominio.Logica.Usuario;
using static MandradePkgs.Conexoes.Mapeamentos.DpoSqlMapper;

namespace Dominio.Representacao.Usuario.Pessoa
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
            cnf.CreateMap<PessoaDom, PessoaDto>()
                .ReverseMap();

            cnf.CreateMap<PessoaDto, PessoaDpo>()
                .ForMember(dpo => dpo.Cpf, opt => opt.MapFrom(v => v.Cpf.ToString()))
                .ForMember(dto => dto.Telefone, opt => opt.MapFrom(v => v.Telefone))
                .ReverseMap();
        }
    }
}