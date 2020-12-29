using Infraestrutura.Repositorio.Usuario.Interface;
using Infraestrutura.Servicos.Usuario.Interface;
using MandradePkgs.Mensagens;
using MandradePkgs.Retornos;
using System;
using System.Linq;
using System.Collections.Generic;
using Infraestrutura.Servico.Usuario.Entidade;
using Infraestrutura.Repositorio.Usuario.Entidade;
using MandradePkgs.Retornos.Erros.Exceptions;
using Dominio.Entidade.Usuario;

namespace Infraestrutura.Servico.Usuario.Implementacao
{
    public class GrupoSv : IGrupoSv
    {
        private IGrupoRp _repositorio { get; }
        private IMensagensApi _mensagens { get; }
        public GrupoSv(IGrupoRp Repositorios, IMensagensApi mensagens)
        {
            _repositorio = Repositorios;
            _mensagens = mensagens;
        }

        public bool InserirNovoUsuario(GrupoInclusaoDto grupo)
        {
            GrupoDpo pai = null;
            bool possuiPai = false;
            if (grupo.Pai.HasValue)
            {
                possuiPai = true;
                pai = _repositorio.ObterGrupoPorId(grupo.Pai.Value);
                if (pai == null)
                    throw new ArgumentException("Pai informado para o novo grupo inválido");
            }
            var dominio = new GrupoDm(0, grupo.Nome, grupo.Descricao, possuiPai ? grupo.Pai.Value : 0);
            dominio.DefinirMensagens(_mensagens);

            dominio.ValidarDados();

            if (_mensagens.PossuiFalhasValidacao())
                throw new RegraNegocioException("Houve erros de validação. Favor verificar notificações.");

            var grupoBanco = new GrupoDpo(0, grupo.Nome, grupo.Descricao, possuiPai ? grupo.Pai.Value : 0);
            var sucesso = _repositorio.AdicionarGrupo(grupoBanco);

            if (!sucesso)
                throw new FalhaExecucaoException("Ocorreu uma falha ao cadastrar este grupo. Tente novamente mais tarde.");

            _mensagens.AdicionarMensagem("Grupo adicionado com sucesso!");
            return sucesso;
        }

        public GrupoDto ObterPai(int id)
        {
            var grupo = _repositorio.ObterPai(id);
            if (grupo == null)
                return null;

            return new GrupoDto(grupo);
        }

        public GrupoDto PesquisarGrupoPorId(int id)
        {
            var grupo = _repositorio.ObterGrupoPorId(id);
            if (grupo == null)
                return null;

            return new GrupoDto(grupo);
        }

        public bool ExcluirGrupo(int id)
        {
            var sucesso = _repositorio.DeletarGrupo(id);
            if (!sucesso)
                throw new FalhaExecucaoException("Não foi possível localizar o grupo. Verifique os dados e tente novamente.");

            _mensagens.AdicionarMensagem($"Grupo foi excluído com sucesso!");
            return sucesso;
        }

        public bool VincularGrupos(int grupoPai, int grupoFilho)
        {
            var sucesso = _repositorio.VincularGrupos(grupoPai, grupoFilho);
            if (!sucesso)
                throw new FalhaExecucaoException("Ocorreu uma falha ao realizar o vínculo, tente novamente mais tarde.");

            _mensagens.AdicionarMensagem($"Grupos foram vinculados com sucesso!");
            return sucesso;
        }

        public List<GrupoDto> ListarFilhos(int id)
        {
            var filhos = _repositorio.ObterFilhos(id);
            if (!filhos.Any())
                return null;

            var listaRetorno = new List<GrupoDto>();
            foreach (var filho in filhos)
                listaRetorno.Add(new GrupoDto(filho));

            return listaRetorno;
        }

        public List<GrupoDto> ListarTodosGrupos(GrupoPesquisaDto filtro)
        {
            var grupos = _repositorio.ObterGrupos(filtro.nome, filtro.descricao);
            if (!grupos.Any())
                return null;

            var listaRetorno = new List<GrupoDto>();
            foreach (var grupo in grupos)
                listaRetorno.Add(new GrupoDto(grupo));

            return listaRetorno;
        }
    }
}
