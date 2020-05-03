using Abstracoes.Representacoes.Usuario.Usuario;
using Abstracoes.Tradutores.Usuario.Interfaces;
using Dominio.Logica.Usuario;
using MandradePkgs.Mensagens;

namespace Abstracoes.Tradutores.Usuario.Implementacoes
{
    public class UsuarioTrd : IUsuarioTrd
    {
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
    }
}