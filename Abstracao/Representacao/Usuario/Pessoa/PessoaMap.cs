using AutoMapper;
using Dominio.Logica.Usuario;
using MandradePkgs.Mensagens;
using static MandradePkgs.Conexoes.Mapeamentos.DpoSqlMapper;

namespace Abstracao.Representacao.Usuario.Pessoa
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
            cnf.CreateMap<(PessoaDto, IMensagensApi), PessoaDom>()
                .ReverseMap();

            cnf.CreateMap<PessoaDto, PessoaDom>()
                .ReverseMap();

            cnf.CreateMap<PessoaDto, PessoaDpo>()
                .ForMember(dpo => dpo.Cpf, opt => opt.MapFrom(v => v.Cpf.ToString()))
                .ForMember(dto => dto.Telefone, opt => opt.MapFrom(v => v.Telefone.ObterValorNumerico()))
                .ReverseMap();
        }
    }
}