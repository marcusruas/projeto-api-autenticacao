using Dominio.Representacao.Pessoa;

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
