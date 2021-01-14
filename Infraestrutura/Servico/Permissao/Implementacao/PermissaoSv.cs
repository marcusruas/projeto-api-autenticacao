using System.Data.SqlClient;
using Dominio.Entidade.Permissao;
using infraestrutura.Servico.Permissao.Entidade;
using Infraestrutura.Repositorio.Permissao.Entidade;
using Infraestrutura.Repositorios.Permissao.Interface;
using Infraestrutura.Servico.Permissao.Interface;
using MandradePkgs.Mensagens;
using Servico.Recurso;

namespace Infraestrutura.Servico.Permissao.Implementacao
{
    public class PermissaoSv : IPermissaoSv
    {
        private IPermissaoRp _repositorio;
        private IMensagensApi _mensagens;

        public PermissaoSv(IPermissaoRp repositorio, IMensagensApi mensagens)
        {
            _repositorio = repositorio;
            _mensagens = mensagens;
        }
        
        public PermissaoDto IncluirPermissao(string descricao)
        {
            var dominio = new PermissaoDm(descricao);
            bool sucesso = false;

            try
            {
                var permissaoBanco = new PermissaoDpo(dominio.Permissao, dominio.Descricao);
                sucesso = _repositorio.InserirPermissao(permissaoBanco);
            }
            catch (SqlException ex)
            {
                _mensagens.AdicionarMensagem(ex.Message);
            }

            if (sucesso)
            {
                _mensagens.AdicionarMensagem(TipoMensagem.Informativo, MensagensErro.PermissaoSucessoInclusao);
                return new PermissaoDto(dominio.Permissao, dominio.Descricao);
            }
            else
            {
                _mensagens.AdicionarMensagem(TipoMensagem.Erro, MensagensErro.PermissaoFalhaInclusao);
                return null;
            }
        }
    }
}