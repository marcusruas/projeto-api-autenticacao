using Abstracoes.Representacoes.Usuario.Pessoa;

namespace Servicos.Usuario.Interfaces
{
    public interface IPessoaSrv
    {
        bool IncluirPessoa(PessoaDto pessoa);
        PessoaDto PesquisarPessoaCpf(string cpf);
        bool AtualizarDadosPessoa(PessoaDto pessoa);
        bool ExcluirPessoa(string nomePessoa);
    }
}
