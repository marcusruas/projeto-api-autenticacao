using Aplicacao.Pessoa;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorio.Pessoa.Interface
{
    public interface IPessoaRep
    {
        bool InserirPessoa(PessoaDbo pessoa);
    }
}
