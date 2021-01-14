using infraestrutura.Servico.Permissao.Entidade;

namespace Infraestrutura.Servico.Permissao.Interface
{
    public interface IPermissaoSv
    {
        PermissaoDto IncluirPermissao(string descricao);
    }
}