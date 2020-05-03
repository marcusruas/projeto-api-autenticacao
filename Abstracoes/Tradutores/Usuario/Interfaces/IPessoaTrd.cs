using Abstracoes.Representacoes.Usuario.Pessoa;
using Dominio.Logica.Usuario;
using MandradePkgs.Mensagens;

namespace Abstracoes.Tradutores.Usuario.Interfaces
{
    public interface IPessoaTrd
    {
        PessoaDto MapearParaDto(PessoaDom pessoa);
        PessoaDto MapearParaDto(PessoaDpo pessoa);
        PessoaDpo MapearParaDpo(PessoaDto pessoa);
        PessoaDom MapearParaDominio(PessoaDto pessoa, IMensagensApi mensagens);
        PessoaDom MapearParaDominio(PessoaDpo pessoa, IMensagensApi mensagens);
    }
}