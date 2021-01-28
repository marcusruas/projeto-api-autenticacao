using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dominio.Entidade.Permissao;
using infraestrutura.Servico.Permissao.Entidade;
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
        
        public PermissaoDto IncluirPermissao(string descricao)
        {
            var dominio = new PermissaoDm(descricao);
            dominio.DefinirMensagens(_mensagens);
            dominio.PossuiCaracteresInvalidos();
            if (_mensagens.PossuiFalhasValidacao())
                throw new RegraNegocioException(MensagensErro.RegraNegocioErroValidacao);

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

        public List<AcessoSistemicoDto> ListarAcessos(int idUsuario)
        {
            var grupoUsuario = _usuarioSv.ObterGrupoUsuario(idUsuario);
            if (grupoUsuario == null || grupoUsuario.Id == 0) {
                //MSG
                _mensagens.AdicionarMensagem(TipoMensagem.Erro, MensagensErro.PermissaoFalhaInclusao);
                return null;
            }

            var acessosUsuario = _repositorio.PesquisarAcessosUsuario(idUsuario);
            var acessosGrupo = _repositorio.PesquisarAcessosGrupo(grupoUsuario.Id);

            List<AcessoSistemicoDto> listaAcessos = new List<AcessoSistemicoDto>();
            acessosUsuario.ForEach(acesso => listaAcessos.Add(new AcessoSistemicoDto(acesso)));
            acessosGrupo.ForEach(acesso => listaAcessos.Add(new AcessoSistemicoDto(acesso)));
            
            return listaAcessos.Distinct<AcessoSistemicoDto>().ToList();
        }
    }
}