using Abstracoes.Representacoes.Usuario.Usuario;
using Repositorios.Usuario.Interfaces;
using Servicos.Usuario.Interfaces;

namespace Servicos.Usuario.Implementacoes
{
    public class UsuarioSrv : IUsuarioSrv
    {
        private IUsuarioRep _usuarioRepositorio;
        private IGrupoSrv _grupoServico;
        private IPessoaSrv _pessoaServico;

        public UsuarioSrv(IUsuarioRep _usuario, IGrupoSrv _grupo, IPessoaSrv _pessoa)
        {
            _usuarioRepositorio = _usuario;
            _grupoServico = _grupo;
            _pessoaServico = _pessoa;
        }

        public void IncluirUsuario(UsuarioInclusaoDto usuario)
        {
            throw new System.NotImplementedException();
        }
    }
}