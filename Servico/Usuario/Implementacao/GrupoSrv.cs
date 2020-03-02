﻿using AutoMapper;
using Repositorio.Usuario.Interface;
using Servico.Usuario.Interface;
using MandradePkgs.Retornos.Erros.Exceptions;
using MandradePkgs.Mensagens;
using System.Collections.Generic;
using System;
using System.Linq;
using Dominio.Representacao.Usuario;
using Dominio.Logica.Usuario;
using Dominio.Representacao.Usuario.Grupo;
using Dominio.ObjetosValor.Enum;

namespace Servico.Usuario.Implementacao
{
    public class GrupoSrv : IGrupoSrv
    {
        private IGrupoRep _repositorio { get; }
        private IMapper _mapper { get; }
        private IMensagensApi _mensagens { get; }
        public GrupoSrv(IGrupoRep repositorio, IMapper mapper, IMensagensApi mensagens)
        {
            _repositorio = repositorio;
            _mapper = mapper;
            _mensagens = mensagens;
        }

        public bool InserirNovoUsuario(GrupoDto grupo)
        {
            var dominio = _mapper.Map<GrupoDom>((grupo, _mensagens));

            dominio.ValidarDados();

            if (_mensagens.PossuiFalhasValidacao())
                throw new RegraNegocioException("Houve erros de validação. Favor verificar notificações.");

            var grupoBanco = _mapper.Map<GrupoDpo>(dominio);
            var sucesso = _repositorio.AdicionarGrupo(grupoBanco);

            if (!sucesso)
                throw new FalhaExecucaoException("Já existe um grupo registrado com este nome.");

            _mensagens.AdicionarMensagem("Grupo adicionado com sucesso!");
            return sucesso;
        }

        public List<GrupoDto> GruposPorNivel(NivelGrupo nivel)
        {
            if (!NivelExiste(nivel))
                throw new RegraNegocioException("Nível informado não existe. Favor selecionar outro.");

            var listaGrupos = _repositorio.ObterGruposPorNivel((int)nivel);

            return _mapper.Map<List<GrupoDto>>(listaGrupos);
        }

        public GrupoDto ObterDadosGrupo(string grupo)
        {
            var grupoBanco = _repositorio.ObterDadosGrupo(grupo);
            return _mapper.Map<GrupoDto>(grupoBanco);
        }

        public bool AtualizarNivelGrupo(string grupo, NivelGrupo nivel, string justificativa)
        {
            if (!NivelExiste(nivel))
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, "Nível informado não existe. Favor selecionar outro.");

            var grupoDto = new GrupoDto();
            grupoDto.Nome = grupo;
            grupoDto.Nivel = (NivelGrupo)nivel;
            grupoDto.Justificativa = justificativa;
            var dominio = _mapper.Map<GrupoDom>((grupoDto, _mensagens));

            dominio.ValidarJustificativa();
            dominio.ValidarJustificativaParaNivel();

            if (_mensagens.PossuiFalhasValidacao())
                throw new RegraNegocioException("Houve erros de validação. Favor verificar notificações.");

            var sucesso = _repositorio.AtualizarNivelGrupo(grupo, (int)nivel, justificativa);
            if (!sucesso)
                throw new FalhaExecucaoException("Não foi possível localizar o grupo. Verifique o nome do grupo e tente novamente.");

            _mensagens.AdicionarMensagem($"Nível do grupo {grupo} foi atualizado com sucesso!");
            return sucesso;
        }

        public bool ExcluirGrupo(string grupo)
        {
            var sucesso = _repositorio.DeletarGrupo(grupo);
            if (!sucesso)
                throw new FalhaExecucaoException("Não foi possível localizar o grupo. Verifique o nome do grupo e tente novamente.");

            _mensagens.AdicionarMensagem($"Grupo {grupo} foi excluído com sucesso!");
            return sucesso;
        }

        private bool NivelExiste(NivelGrupo nivel) =>
            Enum.GetValues(typeof(NivelGrupo)).Cast<NivelGrupo>().Any(grupo => grupo == nivel);
    }
}
