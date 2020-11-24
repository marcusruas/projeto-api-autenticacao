using System.Collections.Generic;
using Abstracoes.Representacoes.Permissoes.Permissao;

namespace Repositorios.Permissoes.Interfaces
{
    public interface IPermissoesRep
    {
        bool InserirAcesso(string acesso);
        bool InserirPermissao(PermissaoDpo permissao);
        bool VincularPermissaoAcesso(int idAcesso, int idPermissao);
        List<PermissaoDpo> PesquisarPermissoes(List<int> permissoes);
        AcessoSistemicoDpo PesquisarAcesso(string descricao);
        AcessoSistemicoDpo PesquisarAcesso(int acesso);
        List<AcessoSistemicoDpo> PesquisarAcessos(string descricao);
        bool InserirAcessoGrupo(int acesso, int grupo);
        bool InserirAcessoUsuario(int acesso, int usuario);
        List<AcessoSistemicoDpo> PesquisarAcessosGrupo(int grupo);
        List<AcessoSistemicoDpo> PesquisarAcessosUsuario(int usuario);
    }
}