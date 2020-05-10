using Abstracoes.Representacoes.Usuario.Grupo;
using Abstracoes.Representacoes.Usuario.Pessoa;
using Abstracoes.Representacoes.Usuario.Usuario;
using Abstracoes.Tradutores.Usuario.Interfaces;
using Dominio.Logica.Usuario;
using MandradePkgs.Mensagens;
using SharedKernel.ObjetosValor.Formatos;

namespace Abstracoes.Tradutores.Usuario.Implementacoes
{
    public class UsuarioTrd : IUsuarioTrd
    {
        private IPessoaTrd _pessoaTradutor;
        private IGrupoTrd _grupoTradutor;
        public UsuarioTrd(IPessoaTrd pessoaTradutor, IGrupoTrd grupoTradutor)
        {
            _pessoaTradutor = pessoaTradutor;
            _grupoTradutor = grupoTradutor;
        }

        public UsuarioDom MapearParaDominio(
            UsuarioInclusaoDto usuario,
            GrupoDom grupo,
            PessoaDom pessoa,
            IMensagensApi mensagens
        ) =>
            new UsuarioDom(
                0,
                usuario.Usuario,
                usuario.Senha,
                grupo,
                pessoa,
                mensagens
            );

        public UsuarioDpo MapearParaDpo(UsuarioDom usuario) =>
            new UsuarioDpo(
                usuario.Id,
                usuario.Usuario,
                usuario.Senha.ValorCriptografado,
                usuario.DataCriacao,
                usuario.Ativo,
                usuario.Grupo.Id,
                usuario.Pessoa.Id
            );

        public UsuarioDto MapearParaDto(UsuarioDpo usuario, GrupoDpo grupo, PessoaDpo pessoa) =>
            new UsuarioDto(
                usuario.Id,
                usuario.Usuario,
                new Senha(usuario.Senha),
                usuario.DataCriacao,
                usuario.Ativo,
                _grupoTradutor.MapearParaDto(grupo),
                _pessoaTradutor.MapearParaDto(pessoa)
            );
    }
}