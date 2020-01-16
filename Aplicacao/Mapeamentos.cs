using Aplicacao.Grupo;
using AutoMapper;
using Dominio.Grupo;

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
                .ReverseMap();
            #endregion
        }
    }
}
