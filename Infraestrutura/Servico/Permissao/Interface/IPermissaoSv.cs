using System.Collections.Generic;
using Infraestrutura.Servico.Permissao.Entidade;

namespace Infraestrutura.Servico.Permissao.Interface
{
    public interface IPermissaoSv
    {
        bool IncluirPermissao(string nome, string descricao);
        List<PermissaoDto> ListarPermissoes(string nome);
        PermissaoDto PesquisarPermissaoPorId(int permissao);
        List<PermissaoDto> ListarPermissoesUsuario(int usuario);
        List<PermissaoDto> ListarPermissoesGrupo(int grupo);
        bool InserirPermissoesUsuario(int usuario, int permissao);
        bool InserirPermissoesGrupo(int grupo, int permissao);
    }
}