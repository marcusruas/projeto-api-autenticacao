using System;
using System.Collections.Generic;
using Infraestrutura.Repositorio.Permissao.Entidade;

namespace Infraestrutura.Repositorio.Permissao.Interface
{
    public interface IPermissaoRp
    {
        bool InserirPermissao(PermissaoDpo permissao);
        List<PermissaoDpo> PesquisarPermissoes(string nome);
        PermissaoDpo PesquisarPermissaoPorId(int permissao);
        List<PermissaoDpo> PesquisarPermissoesUsuario(int usuario);
        List<PermissaoDpo> PesquisarPermissoesGrupo(int grupo);
        bool InserirPermissaoUsuario(int usuario, int permissao);
        bool InserirPermissaoGrupo(int grupo, int permissao);
        bool AtualizarAtividadePermissao(int permissao, bool ativo);
        bool DeletarPermissaoGrupo(int permissao, int grupo);
        bool DeletarPermissaoUsuario(int permissao, int usuario);
    }
}