using System;
using Abstracoes.Representacoes.Usuario.Pessoa;
using Dominio.Logica.Usuario;
using MandradePkgs.Mensagens;
using SharedKernel.ObjetosValor.Formatos;

namespace Abstracoes.Builders.Usuario
{
    public class PessoaBuilder
    {
        private PessoaDom pessoa { get; set; }

        public PessoaBuilder ConstruirObjeto(PessoaDto pessoa)
        {
            this.pessoa = new PessoaDom(
                pessoa.Id,
                pessoa.Nome,
                pessoa.Cpf,
                pessoa.Email,
                pessoa.Telefone
            );

            return this;
        }

        public PessoaBuilder ConstruirObjeto(PessoaInclusaoDto pessoa)
        {
            this.pessoa = new PessoaDom(
                0,
                pessoa.Nome,
                !string.IsNullOrWhiteSpace(pessoa.Cpf) ? new Cpf(pessoa.Cpf) : null,
                pessoa.Email,
                !(string.IsNullOrWhiteSpace(pessoa.Ddd) && string.IsNullOrWhiteSpace(pessoa.Numero)) ?
                    new Telefone(pessoa.Ddd, pessoa.Numero) : null
            );

            return this;
        }

        public PessoaBuilder ConstruirObjeto(PessoaDpo pessoa)
        {
            this.pessoa = new PessoaDom(
                pessoa.Id,
                pessoa.Nome,
                new Cpf(pessoa.Cpf.ToString()),
                pessoa.Email,
                new Telefone(pessoa.Ddd, pessoa.Numero)
            );

            return this;
        }

        public PessoaBuilder AdicionarMensageria(IMensagensApi mensagens)
        {
            if (pessoa != null)
                pessoa.DefinirMensagens(mensagens);
            else
                throw new Exception("Grupo ainda não construido");

            return this;
        }

        public PessoaDom Construir() =>
            this.pessoa;
    }
}