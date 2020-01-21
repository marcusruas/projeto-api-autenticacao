using Aplicacao.Pessoa;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servico.Pessoa.Interface
{
    public interface IPessoaSrv
    {
        bool IncluirPessoa(PessoaDto pessoa);
    }
}
