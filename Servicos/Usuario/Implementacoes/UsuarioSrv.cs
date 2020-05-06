using System;
using Abstracoes.Representacoes.Usuario.Usuario;
using Abstracoes.Tradutores.Usuario.Interfaces;
using MandradePkgs.Mensagens;
using MandradePkgs.Retornos.Erros.Exceptions;
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

        public bool IncluirUsuario(UsuarioInclusaoDto usuario)
        {
            if (usuario.Senha != usuario.ConfirmacaoSenha)
                throw new ArgumentException("Senha e confirmação de senha não são iguais, verifique os dados");

            var grupo = _grupoServico.PesquisarGrupoPorId(usuario.IdGrupo);
            if (grupo == null)
                throw new ArgumentException("Grupo informado para o usuário não encontrado");
            var grupoDom = _grupoTradutor.MapearParaDominio(grupo, _mensagens);

            var pessoa = _pessoaServico.PesquisarPessoaPorId(usuario.IdPessoa);
            if (pessoa == null)
                throw new ArgumentException("Pessoa informada para o usuário não encontrada");
            var pessoaDom = _pessoaTradutor.MapearParaDominio(pessoa, _mensagens);

            var usuarioDom = _tradutor.MapearParaDominio(usuario, grupoDom, pessoaDom, _mensagens);
            usuarioDom.ValidarDados();

            if (_mensagens.PossuiFalhasValidacao())
                throw new RegraNegocioException("Houve erros de validação. Favor verificar notificações.");

            var usuarioBanco = _tradutor.MapearParaDpo(usuarioDom);
            var sucessoInsercao = _usuarioRepositorio.InserirUsuario(usuarioBanco);

            if (!sucessoInsercao)
                _mensagens.AdicionarMensagem(TipoMensagem.Erro, "Não foi possível adicionar o novo usuário. Tente novamente mais tarde.");

            _mensagens.AdicionarMensagem("Usuário Adicionado com sucesso!");
            return sucessoInsercao;
        }
    }
}