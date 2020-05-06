using System.Collections.Generic;
using Abstracoes.Representacoes.Usuario.Pessoa;

namespace Servicos.Usuario.Interfaces
{
    public interface IPessoaSrv
    {
        bool IncluirPessoa(PessoaDto pessoa);
        List<PessoaDto> PesquisarPessoas(FiltroBuscaPessoasDto filtro);
        PessoaDto PesquisarPessoaPorId(int id);
        bool AtualizarDadosPessoa(PessoaDto pessoa);
        bool ExcluirPessoa(int id);
    }
}
