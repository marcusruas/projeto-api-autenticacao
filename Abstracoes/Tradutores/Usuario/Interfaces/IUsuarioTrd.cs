using Abstracoes.Representacoes.Usuario.Grupo;
using Abstracoes.Representacoes.Usuario.Pessoa;
using Abstracoes.Representacoes.Usuario.Usuario;
using Dominio.Logica.Usuario;
using MandradePkgs.Mensagens;

namespace Abstracoes.Tradutores.Usuario.Interfaces
{
    public interface IUsuarioTrd
    {
        UsuarioDom MapearParaDominio(UsuarioInclusaoDto usuario, GrupoDom grupo, PessoaDom pessoa, IMensagensApi mensagens);
        UsuarioDpo MapearParaDpo(UsuarioDom usuario);
    }
}