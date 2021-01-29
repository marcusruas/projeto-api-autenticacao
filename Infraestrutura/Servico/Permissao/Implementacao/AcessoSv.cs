using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dominio.Entidade.Permissao;
using Infraestrutura.Servico.Permissao.Entidade;
using Infraestrutura.Repositorio.Permissao.Entidade;
using Infraestrutura.Repositorio.Permissao.Interface;
using Infraestrutura.Servico.Permissao.Interface;
using Infraestrutura.Servico.Usuario.Interface;
using MandradePkgs.Mensagens;
using MandradePkgs.Retornos.Erros.Exceptions;
using Servico.Recurso;

namespace Infraestrutura.Servico.Permissao.Implementacao
{
    public class AcessoSv : IAcessoSv
    {
        private IAcessoRp _repositorio;
        private IPermissaoRp _permissaoRepositorio;
        private IUsuarioSv _usuarioSv;
        private IMensagensApi _mensagens;

        public AcessoSv(IAcessoRp repositorio, IPermissaoRp permissaoRepositorio, IUsuarioSv usuarioSv, IMensagensApi mensagens)
        {
            _repositorio = repositorio;
            _mensagens = mensagens;
            _usuarioSv = usuarioSv;
            _permissaoRepositorio = permissaoRepositorio;
        }

        public bool IncluirAcesso(string descricao, List<int> permissoes)
        {
            List<PermissaoDto> dadosPermissoes = PesquisarPermissoes(permissoes);
            List<int> permissoesNaoEncontradas = permissoes.Where(p => !dadosPermissoes.Any(pp => pp.Id == p)).ToList();

            if (!permissoes.Any())
            {
                _mensagens.AdicionarMensagem(MensagensErro.AcessoPermissoesNaoEncontradas);
                return false;
            }

            foreach (var permissao in permissoesNaoEncontradas)
                _mensagens.AdicionarMensagem(TipoMensagem.Alerta, MensagensErro.PermissaoNaoEncontrada.Replace("N", permissao.ToString()));

            AcessoSistemicoDm dominio = new AcessoSistemicoDm(descricao);
            dominio.PossuiCaracteresInvalidos();
            if (_mensagens.PossuiFalhasValidacao())
                throw new RegraNegocioException(MensagensErro.RegraNegocioErroValidacao);

            bool sucesso = _repositorio.InserirAcesso(dominio.Descricao);

            if (!sucesso)
            {
                _mensagens.AdicionarMensagem(MensagensErro.AcessoJaExiste);
                return sucesso;
            }

            var acesso = _repositorio.PesquisarAcesso(descricao);
            foreach (var permissao in dadosPermissoes.Select(p => p.Id).OrderBy(p => p))
                _repositorio.VincularPermissaoAcesso(acesso.Id, permissao);

            _mensagens.AdicionarMensagem(MensagensErro.AcessoSucessoInclusao);
            return sucesso;
        }

        public List<AcessoSistemicoDto> ListarAcessosUsuario(int idUsuario)
        {
            var grupoUsuario = _usuarioSv.ObterGrupoUsuario(idUsuario);
            if (grupoUsuario == null || grupoUsuario.Id == 0) {
                _mensagens.AdicionarMensagem(TipoMensagem.Erro, MensagensErro.UsuarioGrupoInvalido);
                return new List<AcessoSistemicoDto>();
            }

            var acessosUsuario = _repositorio.PesquisarAcessosUsuario(idUsuario);
            var acessosGrupo = _repositorio.PesquisarAcessosGrupo(grupoUsuario.Id);

            List<AcessoSistemicoDto> listaAcessos = new List<AcessoSistemicoDto>();
            acessosUsuario.ForEach(acesso => listaAcessos.Add(new AcessoSistemicoDto(acesso)));
            acessosGrupo.ForEach(acesso => listaAcessos.Add(new AcessoSistemicoDto(acesso)));
            
            return listaAcessos.Distinct<AcessoSistemicoDto>().ToList();
        }

        private List<PermissaoDto> PesquisarPermissoes(List<int> permissoes)
        {
            var listaPermissoes = _permissaoRepositorio.PesquisarPermissoes(permissoes);
            List<PermissaoDto> retorno = new List<PermissaoDto>();

            foreach (var permissao in listaPermissoes)
                retorno.Add(new PermissaoDto(permissao));

            return retorno;
        }
    }
}