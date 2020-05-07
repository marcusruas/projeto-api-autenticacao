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
using Abstracoes.Tradutores.Usuario.Interfaces;

namespace Servicos.Usuario.Implementacoes
{
    public class GrupoSrv : IGrupoSrv
    {
        private IGrupoRep _repositorio { get; }
        private IGrupoTrd _tradutor { get; }
        private IMensagensApi _mensagens { get; }
        public GrupoSrv(IGrupoRep Repositorios, IMensagensApi mensagens, IGrupoTrd trd)
        {
            _repositorio = Repositorios;
            _tradutor = trd;
            _mensagens = mensagens;
        }

        public bool InserirNovoUsuario(GrupoDto grupo)
        {
            var dominio = _tradutor.MapearParaDominio(grupo, _mensagens);

            dominio.ValidarDados();

            if (_mensagens.PossuiFalhasValidacao())
                throw new RegraNegocioException("Houve erros de validação. Favor verificar notificações.");

            var grupoBanco = _tradutor.MapearParaDpo(grupo);
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

            var resultado = new List<GrupoDto>();

            foreach (var grupo in listaGrupos)
                resultado.Add(_tradutor.MapearParaDto(grupo));

            return resultado;
        }

        public GrupoDto ObterDadosGrupo(string grupo)
        {
            var grupoBanco = _repositorio.ObterDadosGrupo(grupo);
            if (grupoBanco == null)
                _mensagens.AdicionarMensagem(TipoMensagem.Informativo, "Não foi localizado nenhum grupo com esse nome.");
            return _tradutor.MapearParaDto(grupoBanco);
        }

        public bool AtualizarNivelGrupo(string grupo, NivelGrupo nivel, string justificativa)
        {
            if (!NivelExiste(nivel))
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, "Nível informado não existe. Favor selecionar outro.");

            var grupoDto = new GrupoDto();
            grupoDto.Nome = grupo;
            grupoDto.Nivel = (NivelGrupo)nivel;
            grupoDto.Justificativa = justificativa;
            var dominio = _tradutor.MapearParaDominio(grupoDto, _mensagens);

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

        public bool ExcluirGrupo(int id)
        {
            var sucesso = _repositorio.DeletarGrupo(id);
            if (!sucesso)
                throw new FalhaExecucaoException("Não foi possível localizar o grupo. Verifique o nome do grupo e tente novamente.");

            _mensagens.AdicionarMensagem($"Grupo foi excluído com sucesso!");
            return sucesso;
        }

        private bool NivelExiste(NivelGrupo nivel) =>
            Enum.GetValues(typeof(NivelGrupo)).Cast<NivelGrupo>().Any(grupo => grupo == nivel);

        public GrupoDto PesquisarGrupoPorId(int id)
        {
            var grupo = _repositorio.ObterGrupoPorId(id);
            if (grupo == null)
                return null;
            return _tradutor.MapearParaDto(grupo);
        }
    }
}
