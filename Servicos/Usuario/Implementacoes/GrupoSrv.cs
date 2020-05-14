using Repositorios.Usuario.Interfaces;
using Servicos.Usuario.Interfaces;
using MandradePkgs.Retornos.Erros.Exceptions;
using MandradePkgs.Mensagens;
using System;
using System.Linq;
using Abstracoes.Representacoes.Usuario.Grupo;
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

        public bool ExcluirGrupo(int id)
        {
            var sucesso = _repositorio.DeletarGrupo(id);
            if (!sucesso)
                throw new FalhaExecucaoException("Não foi possível localizar o grupo. Verifique os dados e tente novamente.");

            _mensagens.AdicionarMensagem($"Grupo foi excluído com sucesso!");
            return sucesso;
        }

        public GrupoDto PesquisarGrupoPorId(int id)
        {
            var grupo = _repositorio.ObterGrupoPorId(id);
            if (grupo == null)
                return null;
            return _tradutor.MapearParaDto(grupo);
        }
    }
}
