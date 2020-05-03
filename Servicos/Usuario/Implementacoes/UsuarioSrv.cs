using System;
using Abstracoes.Representacoes.Usuario.Usuario;
using Abstracoes.Tradutores.Usuario.Interfaces;
using MandradePkgs.Mensagens;
using Repositorios.Usuario.Interfaces;
using Servicos.Usuario.Interfaces;

namespace Servicos.Usuario.Implementacoes
{
    public class UsuarioSrv : IUsuarioSrv
    {
        private IUsuarioRep _usuarioRepositorio;
        private IGrupoSrv _grupoServico;
        private IPessoaSrv _pessoaServico;
        private IUsuarioTrd _tradutor;
        private IGrupoTrd _grupoTradutor;
        private IPessoaTrd _pessoaTradutor;
        private IMensagensApi _mensagens;

        public UsuarioSrv(
            IUsuarioRep _usuario,
            IGrupoSrv _grupo,
            IPessoaSrv _pessoa,
            IUsuarioTrd tradutor,
            IGrupoTrd grupoTradutor,
            IPessoaTrd pessoaTradutor,
            IMensagensApi mensagens
        )
        {
            _usuarioRepositorio = _usuario;
            _grupoServico = _grupo;
            _pessoaServico = _pessoa;
            _tradutor = tradutor;
            _pessoaTradutor = pessoaTradutor;
            _grupoTradutor = grupoTradutor;
            _mensagens = mensagens;
        }

        public void IncluirUsuario(UsuarioInclusaoDto usuario)
        {
            var grupo = _grupoServico.PesquisarGrupoPorId(usuario.IdGrupo);
            if (grupo == null)
                throw new ArgumentException("Grupo informado para o usuário não encontrado");

            var pessoa = _pessoaServico.PesquisarPessoaPorId(usuario.IdPessoa);
            if (pessoa == null)
                throw new ArgumentException("Pessoa informada para o usuário não encontrada");

            var grupoDom = _grupoTradutor.MapearParaDominio(grupo, _mensagens);
            var pessoaDom = _pessoaTradutor.MapearParaDominio(pessoa, _mensagens);
            var usuarioDom = _tradutor.MapearParaDominio(usuario, grupoDom, pessoaDom, _mensagens);

            usuarioDom.ValidarDados();

            
        }
    }
}