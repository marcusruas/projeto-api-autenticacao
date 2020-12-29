using System.Collections.Generic;
using Infraestrutura.Repositorio.Usuario.Entidade;

namespace Repositorios.Usuario.Interfaces
{
    public interface IPessoaRep
    {
        bool InserirPessoa(PessoaDpo pessoa);
        List<PessoaDpo> BuscarPessoas(string nome, string cpf);
        PessoaDpo ObterPessoaPorId(int id);
        bool UpdateDadosPessoa(PessoaDpo pessoa);
        bool DeletarPessoa(int id);
    }
}
