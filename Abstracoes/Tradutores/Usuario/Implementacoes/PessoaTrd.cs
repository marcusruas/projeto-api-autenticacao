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

        public PessoaDpo MapearParaDpo(PessoaDto pessoa)
        {
            long cpf = pessoa.Cpf == null ?
                0 : long.Parse(pessoa.Cpf.ValorNumerico);
            bool possuiTelefone = pessoa.Telefone != null;

            return new PessoaDpo(
                pessoa.Id,
                pessoa.Nome,
                cpf,
                pessoa.Email,
                possuiTelefone ? pessoa.Telefone.Ddd : null,
                possuiTelefone ? pessoa.Telefone.Numero : null
            );
        }

        public PessoaDto MapearParaDto(PessoaDom pessoa) =>
            new PessoaDto(
                pessoa.Id,
                pessoa.Nome,
                pessoa.Cpf,
                pessoa.Email,
                pessoa.Telefone
            );

        public PessoaDto MapearParaDto(PessoaDpo pessoa)
        {
            Cpf cpf = pessoa.Cpf == 0 ?
                null : new Cpf(pessoa.Cpf.ToString());

            Telefone telefone =
                string.IsNullOrWhiteSpace(pessoa.Ddd) && string.IsNullOrWhiteSpace(pessoa.Numero) ?
                null : new Telefone(pessoa.Ddd, pessoa.Numero);

            return new PessoaDto(
                pessoa.Id,
                pessoa.Nome,
                cpf,
                pessoa.Email,
                telefone
            );
        }
    }
}