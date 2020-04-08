using Abstracoes.Representacoes.Usuario.Pessoa;

namespace Repositorios.Usuario.Interfaces
{
    public interface IPessoaRep
    {
        bool InserirPessoa(PessoaDpo pessoa);
        PessoaDpo BuscarPessoaCpf(string cpf);
        bool UpdateDadosPessoa(PessoaDpo pessoa);
        bool DeletarPessoa(string nomePessoa);
    }
}
