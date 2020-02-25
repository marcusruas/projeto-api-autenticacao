using Aplicacao.Pessoa;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorio.Pessoa.Interface
{
    public interface IPessoaRep
    {
        bool InserirPessoa(PessoaDpo pessoa);
        PessoaDpo BuscarPessoaCpf(string cpf);
        bool UpdateDadosPessoa(PessoaDpo pessoa);
        bool DeletarPessoa(string nomePessoa);
    }
}
