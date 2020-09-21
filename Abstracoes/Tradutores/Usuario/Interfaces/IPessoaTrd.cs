using Abstracoes.Representacoes.Usuario.Pessoa;
using Dominio.Logica.Usuario;
using MandradePkgs.Mensagens;

namespace Abstracoes.Tradutores.Usuario.Interfaces
{
    public interface IPessoaTrd
    {
        PessoaDom MapearParaDominio(PessoaDto pessoa, IMensagensApi mensagens);
        PessoaDom MapearParaDominio(PessoaDpo pessoa, IMensagensApi mensagens);
        PessoaDom MapearParaDominio(PessoaInclusaoDto pessoa, IMensagensApi mensagens);
    }
}