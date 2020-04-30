using System.Collections.Generic;
using Abstracoes.Representacoes.Usuario.Pessoa;
using SharedKernel.ObjetosValor.Formatos;

namespace Repositorios.Usuario.Interfaces
{
    public interface IPessoaRep
    {
        bool InserirPessoa(PessoaDpo pessoa);
        List<PessoaDpo> BuscarPessoas(FiltroBuscaPessoasDto filtro);
        PessoaDpo ObterPessoaPorId(int id);
        bool UpdateDadosPessoa(PessoaDpo pessoa);
        bool DeletarPessoa(string nomePessoa);
    }
}
