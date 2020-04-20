using Abstracoes.Representacoes.Usuario.Pessoa;
using SharedKernel.ObjetosValor.Formatos;

namespace Repositorios.Usuario.Interfaces
{
    public interface IPessoaRep
    {
        bool InserirPessoa(PessoaDpo pessoa);
        PessoaDpo BuscarPessoaCpf(Cpf cpf);
        bool UpdateDadosPessoa(PessoaDpo pessoa);
        bool DeletarPessoa(string nomePessoa);
    }
}
