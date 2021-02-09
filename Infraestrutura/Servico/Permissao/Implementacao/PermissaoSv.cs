using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dominio.Entidade.Permissao;
using Infraestrutura.Repositorio.Permissao.Entidade;
using Infraestrutura.Repositorio.Permissao.Interface;
using Infraestrutura.Servico.Permissao.Entidade;
using Infraestrutura.Servico.Permissao.Interface;
using Infraestrutura.Servico.Usuario.Interface;
using MandradePkgs.Mensagens;
using MandradePkgs.Retornos.Erros.Exceptions;
using Servico.Recurso;

namespace Infraestrutura.Servico.Permissao.Implementacao
{
    public class PermissaoSv : IPermissaoSv
    {
        private IPermissaoRp _repositorio;
        private IUsuarioSv _usuarioSv;
        private IMensagensApi _mensagens;

        public PermissaoSv(IPermissaoRp repositorio, IUsuarioSv usuarioSv, IMensagensApi mensagens)
        {
            _repositorio = repositorio;
            _mensagens = mensagens;
            _usuarioSv = usuarioSv;
        }

        public bool IncluirPermissao(string nome, string descricao)
        {
            var dominio = new PermissaoDm(nome, descricao);
            dominio.DefinirMensagens(_mensagens);
            dominio.PossuiCaracteresInvalidos();
            
            if (_mensagens.PossuiFalhasValidacao())
                throw new RegraNegocioException(MensagensErro.RegraNegocioErroValidacao);

            bool sucesso = false;

            try
            {
                var permissaoBanco = new PermissaoDpo(dominio.Permissao, dominio.Nome, dominio.Descricao, dominio.Ativo);
                sucesso = _repositorio.InserirPermissao(permissaoBanco);

                if (sucesso)
                {
                    _mensagens.AdicionarMensagem(TipoMensagem.Informativo, MensagensErro.PermissaoSucessoInclusao);
                    return sucesso;
                }
                
                _mensagens.AdicionarMensagem(TipoMensagem.Erro, MensagensErro.PermissaoFalhaInclusao);
                return sucesso;
            }
            catch (SqlException ex)
            {
                _mensagens.AdicionarMensagem(TipoMensagem.Erro, ex.Message);
                return false;
            }
        }

        public List<PermissaoDto> ListarPermissoes(string nome)
        {
            var consulta = _repositorio.PesquisarPermissoes(nome);
            return CastToDto(consulta);
        }

        public List<PermissaoDto> ListarPermissoesUsuario(int usuario)
        {
            var consulta = _repositorio.PesquisarPermissoesUsuario(usuario);

            if(!consulta.Any())
                _mensagens.AdicionarMensagem(TipoMensagem.Alerta, MensagensErro.PesquisaSemResultados);
                
            return CastToDto(consulta);
        }

        public PermissaoDto PesquisarPermissaoPorId(int permissao)
        {
            var permissaoBanco = _repositorio.PesquisarPermissaoPorId(permissao);

            if(permissaoBanco == null) {
                _mensagens.AdicionarMensagem(TipoMensagem.Erro, MensagensErro.PesquisaSemResultados);
                return null;
            }

            return new PermissaoDto(permissaoBanco);
        }

        public List<PermissaoDto> ListarPermissoesGrupo(int grupo)
        {
            var consulta = _repositorio.PesquisarPermissoesGrupo(grupo);

            if(!consulta.Any())
                _mensagens.AdicionarMensagem(TipoMensagem.Alerta, MensagensErro.PesquisaSemResultados);
                
            return CastToDto(consulta);
        }

        public bool InserirPermissoesUsuario(int usuario, int permissao)
        {
            bool sucesso = _repositorio.InserirPermissaoUsuario(usuario, permissao);

            if (!sucesso) {
                _mensagens.AdicionarMensagem(TipoMensagem.Erro, "Não foi possível cadastrar a permissão, verifique os dados e tente novamente mais tarde");
                return sucesso;
            }

            _mensagens.AdicionarMensagem(TipoMensagem.Erro, "Permissão cadastrada com sucesso!");
            return sucesso;
         }

        public bool InserirPermissoesGrupo(int grupo, int permissao)
        {
            bool sucesso = _repositorio.InserirPermissaoGrupo(grupo, permissao);

            if (!sucesso) {
                _mensagens.AdicionarMensagem(TipoMensagem.Erro, "Não foi possível cadastrar a permissão, verifique os dados e tente novamente mais tarde");
                return sucesso;
            }

            _mensagens.AdicionarMensagem(TipoMensagem.Erro, "Permissão cadastrada com sucesso!");
            return sucesso;
        }

        public bool AtualizarAtividadePermissao(int permissao, bool ativo)
        {
            bool sucesso = _repositorio.AtualizarAtividadePermissao(permissao, ativo);

            if (!sucesso) {
                _mensagens.AdicionarMensagem(TipoMensagem.Erro, "Não foi possível alterar a atividade da permissão, verifique os dados e tente novamente mais tarde");
                return sucesso;
            }

            _mensagens.AdicionarMensagem(TipoMensagem.Erro, "Permissão alterada com sucesso!");
            return sucesso;
        }

        public bool ExcluirPermissaoGrupo(int permissao, int grupo)
        {
            bool sucesso = _repositorio.DeletarPermissaoGrupo(permissao, grupo);

            if (!sucesso) {
                _mensagens.AdicionarMensagem(TipoMensagem.Erro, "Não foi possível excluir permissão indicada, verifique os dados e tente novamente mais tarde");
                return sucesso;
            }

            _mensagens.AdicionarMensagem(TipoMensagem.Erro, "Permissão excluída com sucesso!");
            return sucesso;
        }

        public bool ExcluirPermissaoUsuario(int permissao, int usuario)
        {
            bool sucesso = _repositorio.DeletarPermissaoUsuario(permissao, usuario);

            if (!sucesso) {
                _mensagens.AdicionarMensagem(TipoMensagem.Erro, "Não foi possível excluir permissão indicada, verifique os dados e tente novamente mais tarde");
                return sucesso;
            }

            _mensagens.AdicionarMensagem(TipoMensagem.Erro, "Permissão excluída com sucesso!");
            return sucesso;
        }

        public List<PermissaoDto> CastToDto(List<PermissaoDpo> lista) {
            List<PermissaoDto> retorno = new List<PermissaoDto>();
            lista.ForEach(reg => retorno.Add(new PermissaoDto(reg)));

            return retorno;
        }
    }
}