using System;
using System.Collections.Generic;
using System.Text;
using Dominio.Representacoes.Pessoa;

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
