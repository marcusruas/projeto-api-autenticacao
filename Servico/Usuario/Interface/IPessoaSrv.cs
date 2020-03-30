using Abstracao.Representacao.Usuario.Pessoa;

namespace Servico.Usuario.Interface
{
    public interface IPessoaSrv
    {
        bool IncluirPessoa(PessoaDto pessoa);
        PessoaDto PesquisarPessoaCpf(string cpf);
        bool AtualizarDadosPessoa(PessoaDto pessoa);
        bool ExcluirPessoa(string nomePessoa);
    }
}
