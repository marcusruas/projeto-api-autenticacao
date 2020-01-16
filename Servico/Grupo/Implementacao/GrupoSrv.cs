using AutoMapper;
using Dominio.Grupo;
using Repositorio.Grupo.Interface;
using Servico.Grupo.Interface;
using MandradePkgs.Retornos.Erros.Exceptions;
using Aplicacao.Grupo;

namespace Servico.Grupo.Implementacao
{
    public class GrupoSrv : IGrupoSrv
    {
        private IGrupoRep _repositorio { get; }
        private IMapper _mapper { get; }
        public GrupoSrv(IGrupoRep repositorio, IMapper mapper) {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public bool InserirNovoUsuario(string nome, int nivel) {
            var dominio = new GrupoDom(nome, nivel);
            if (!dominio.NomeValido())
                throw new RegraNegocioException("Nome do grupo deve conter mais de 5 caractéres e não possuir números");

            var grupo = _mapper.Map<GrupoDbo>(dominio);
            return _repositorio.AdicionarGrupo(grupo);
        }
    }
}
