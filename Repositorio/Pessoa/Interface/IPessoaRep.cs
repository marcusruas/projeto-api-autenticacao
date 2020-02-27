using Dominio.Representacao.Pessoa;

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
