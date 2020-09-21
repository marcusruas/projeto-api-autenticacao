using Abstracoes.Representacoes.Usuario.Pessoa;
using Abstracoes.Tradutores.Usuario.Interfaces;
using Dominio.Logica.Usuario;
using MandradePkgs.Mensagens;
using SharedKernel.ObjetosValor.Formatos;

namespace Abstracoes.Tradutores.Usuario.Implementacoes
{
    public class PessoaTrd : IPessoaTrd
    {
        public PessoaDom MapearParaDominio(PessoaDto pessoa, IMensagensApi mensagens) =>
            new PessoaDom(
                pessoa.Id,
                pessoa.Nome,
                pessoa.Cpf,
                pessoa.Email,
                pessoa.Telefone,
                mensagens
            );

        public PessoaDom MapearParaDominio(PessoaInclusaoDto pessoa, IMensagensApi mensagens) =>
            new PessoaDom(
                0,
                pessoa.Nome,
                !string.IsNullOrWhiteSpace(pessoa.Cpf) ? new Cpf(pessoa.Cpf) : null,
                pessoa.Email,
                !(string.IsNullOrWhiteSpace(pessoa.Ddd) && string.IsNullOrWhiteSpace(pessoa.Numero)) ?
                    new Telefone(pessoa.Ddd, pessoa.Numero) : null,
                mensagens
            );

        public PessoaDom MapearParaDominio(PessoaDpo pessoa, IMensagensApi mensagens) =>
            new PessoaDom(
                pessoa.Id,
                pessoa.Nome,
                new Cpf(pessoa.Cpf.ToString()),
                pessoa.Email,
                new Telefone(pessoa.Ddd, pessoa.Numero),
                mensagens
            );
    }
}