using Aplicacao.Pessoa;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servico.Pessoa.Interface
{
    public interface IPessoaSrv
    {
        bool IncluirPessoa(PessoaDto pessoa);
        PessoaDto PesquisarPessoaCpf(string cpf);
        bool AtualizarDadosPessoa(PessoaDto pessoa);
        bool ExcluirPessoa(string nomePessoa);
    }
}
