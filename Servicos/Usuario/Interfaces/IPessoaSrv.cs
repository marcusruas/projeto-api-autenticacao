using System.Collections.Generic;
using Abstracoes.Representacoes.Usuario.Pessoa;

namespace Servicos.Usuario.Interfaces
{
    public interface IPessoaSrv
    {
        bool IncluirPessoa(PessoaDto pessoa);
        List<PessoaDto> PesquisarPessoas(FiltroBuscaPessoasDto filtro);
        bool AtualizarDadosPessoa(PessoaDto pessoa);
        bool ExcluirPessoa(string nomePessoa);
    }
}
