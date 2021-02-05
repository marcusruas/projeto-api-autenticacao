using System.Collections.Generic;
using System.Data.SqlClient;
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
            List<PermissaoDto> retorno = new List<PermissaoDto>();

            consulta.ForEach(item => retorno.Add(new PermissaoDto(item)));
            return retorno;
        }
    }
}