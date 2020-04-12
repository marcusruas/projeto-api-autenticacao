using AutoMapper;
using Repositorios.Usuario.Interfaces;
using Servicos.Usuario.Interfaces;
using MandradePkgs.Retornos.Erros.Exceptions;
using MandradePkgs.Mensagens;
using System.Collections.Generic;
using System;
using System.Linq;
using Dominio.Logica.Usuario;
using Abstracoes.Representacoes.Usuario.Grupo;
using SharedKernel.ObjetosValor.Enum;

namespace Servicos.Usuario.Implementacoes
{
    public class GrupoSrv : IGrupoSrv
    {
        private IGrupoRep _Repositorios { get; }
        private IMapper _mapper { get; }
        private IMensagensApi _mensagens { get; }
        public GrupoSrv(IGrupoRep Repositorios, IMensagensApi mensagens)
        {
            _Repositorios = Repositorios;
            _mapper = null;
            _mensagens = mensagens;
        }

        public bool InserirNovoUsuario(GrupoDto grupo)
        {
            var dominio = _mapper.Map<GrupoDom>((grupo, _mensagens));

            dominio.ValidarDados();

            if (_mensagens.PossuiFalhasValidacao())
                throw new RegraNegocioException("Houve erros de validação. Favor verificar notificações.");

            var grupoBanco = _mapper.Map<GrupoDpo>(dominio);
            var sucesso = _Repositorios.AdicionarGrupo(grupoBanco);

            if (!sucesso)
                throw new FalhaExecucaoException("Já existe um grupo registrado com este nome.");

            _mensagens.AdicionarMensagem("Grupo adicionado com sucesso!");
            return sucesso;
        }

        public List<GrupoDto> GruposPorNivel(NivelGrupo nivel)
        {
            if (!NivelExiste(nivel))
                throw new RegraNegocioException("Nível informado não existe. Favor selecionar outro.");

            var listaGrupos = _Repositorios.ObterGruposPorNivel((int)nivel);

            return _mapper.Map<List<GrupoDto>>(listaGrupos);
        }

        public GrupoDto ObterDadosGrupo(string grupo)
        {
            var grupoBanco = _Repositorios.ObterDadosGrupo(grupo);
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

            var sucesso = _Repositorios.AtualizarNivelGrupo(grupo, (int)nivel, justificativa);
            if (!sucesso)
                throw new FalhaExecucaoException("Não foi possível localizar o grupo. Verifique o nome do grupo e tente novamente.");

            _mensagens.AdicionarMensagem($"Nível do grupo {grupo} foi atualizado com sucesso!");
            return sucesso;
        }

        public bool ExcluirGrupo(string grupo)
        {
            var sucesso = _Repositorios.DeletarGrupo(grupo);
            if (!sucesso)
                throw new FalhaExecucaoException("Não foi possível localizar o grupo. Verifique o nome do grupo e tente novamente.");

            _mensagens.AdicionarMensagem($"Grupo {grupo} foi excluído com sucesso!");
            return sucesso;
        }

        private bool NivelExiste(NivelGrupo nivel) =>
            Enum.GetValues(typeof(NivelGrupo)).Cast<NivelGrupo>().Any(grupo => grupo == nivel);
    }
}
