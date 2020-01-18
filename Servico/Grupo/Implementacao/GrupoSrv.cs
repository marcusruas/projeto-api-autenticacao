using AutoMapper;
using Dominio.Grupo;
using Repositorio.Grupo.Interface;
using Servico.Grupo.Interface;
using MandradePkgs.Retornos.Erros.Exceptions;
using Aplicacao.Grupo;
using MandradePkgs.Mensagens;
using System.Collections.Generic;
using System;
using System.Linq;

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

        public bool InserirNovoUsuario(string nome, string descricao, NivelGrupo nivel) {
            try {
                var dominio = new GrupoDom(nome, descricao, nivel);
                if (!dominio.NomeValido())
                    throw new RegraNegocioException("Nome do grupo deve conter mais de 5 caractéres e não possuir números.");
                var grupo = _mapper.Map<GrupoDbo>(dominio);

                var sucesso = _repositorio.AdicionarGrupo(grupo);

                if (!sucesso)
                    throw new RegraNegocioException("Já existe um grupo registrado com este nome.");

                _mensagens.AdicionarMensagem("Grupo adicionado com sucesso!");

                return sucesso;
            }catch(Exception ex) {
                _mensagens.AdicionarMensagem(TipoMensagem.Erro, ex.Message);
                throw new FalhaExecucaoException(ex.Message);
            }
        }

        public List<GrupoDom> GruposPorNivel(int nivel) {
            try {
                if (!GrupoExiste(nivel))
                    throw new RegraNegocioException("Nível informado não existe. Favor selecionar outro.");

                var listaGrupos = _repositorio.ObterGruposPorNivel(nivel);

                return _mapper.Map<List<GrupoDom>>(listaGrupos);
            } catch (Exception ex) {
                _mensagens.AdicionarMensagem(TipoMensagem.Erro, ex.Message);
                throw new FalhaExecucaoException(ex.Message);
            }
        }

        public bool AtualizarNivelGrupo(string grupo, int nivel) {
            if (!GrupoExiste(nivel))
                throw new RegraNegocioException("Nível informado não existe. Favor selecionar outro.");

            var sucesso = _repositorio.AtualizarNivelGrupo(grupo, nivel);
            if (!sucesso)
                throw new RegraNegocioException("Não foi possível localizar o grupo. Verifique o nome do grupo e tente novamente.");

            _mensagens.AdicionarMensagem("Nível do grupo atualizado com sucesso!");
            return sucesso;
        }

        private bool GrupoExiste(int nivel) =>
            Enum.GetValues(typeof(NivelGrupo)).Cast<int>().Any(grupo => grupo == nivel);
    }
}
