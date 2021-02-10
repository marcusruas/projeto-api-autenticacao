using System.Collections.Generic;
using Infraestrutura.Servico.Usuario.Entidade;

namespace Infraestrutura.Servico.Usuario.Interface
{
    public interface IPessoaSv
    {
        bool IncluirPessoa(PessoaInclusaoDto pessoa);
        List<PessoaDto> PesquisarPessoas(FiltroBuscaPessoasDto filtro);
        PessoaDto PesquisarPessoaPorId(int id);
        bool AtualizarDadosPessoa(PessoaAlteracaoDto pessoa);
        bool ExcluirPessoa(int id);
    }
}
