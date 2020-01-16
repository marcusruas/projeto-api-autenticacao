using AutoMapper;
using Dominio.Grupo;
using Repositorio.Grupo.Interface;
using Servico.Grupo.Interface;
using MandradePkgs.Retornos.Erros.Exceptions;
using Aplicacao.Grupo;
using MandradePkgs.Mensagens;

namespace Servico.Grupo.Implementacao
{
    public class GrupoSrv : IGrupoSrv
    {
        private IGrupoRep _repositorio { get; }
        private IMapper _mapper { get; }
        private IMensagensApi _mensagens { get; }
        public GrupoSrv(IGrupoRep repositorio, IMapper mapper, IMensagensApi mensagens) {
            _repositorio = repositorio;
            _mapper = mapper;
            _mensagens = mensagens;
        }

        public bool InserirNovoUsuario(string nome, int nivel) {
            var dominio = new GrupoDom(nome, nivel);
            if (!dominio.NomeValido())
                throw new RegraNegocioException("Nome do grupo deve conter mais de 5 caractéres e não possuir números.");
            var grupo = _mapper.Map<GrupoDbo>(dominio);

            var sucesso = _repositorio.AdicionarGrupo(grupo);

            if (!sucesso)
                throw new RegraNegocioException("Já existe um grupo registrado com este nome.");

            _mensagens.AdicionarMensagem("Grupo adicionado com sucesso!");

            return sucesso;
        }
    }
}
