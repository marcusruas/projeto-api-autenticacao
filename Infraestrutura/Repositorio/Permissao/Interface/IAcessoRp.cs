using System;
using System.Collections.Generic;
using Infraestrutura.Repositorio.Permissao.Entidade;
using Infraestrutura.Servico.Permissao.Entidade;

namespace Infraestrutura.Repositorio.Permissao.Interface
{
    public interface IAcessoRp
    {
        bool InserirAcesso(string descricao);
        List<AcessoSistemicoDpo> PesquisarAcessosUsuario(int idUsuario);
        List<AcessoSistemicoDpo> PesquisarAcessosGrupo(int idGrupo);
        AcessoSistemicoDpo PesquisarAcesso(string descricao);
        AcessoSistemicoDpo PesquisarAcesso(int idAcesso);
        bool VincularPermissaoAcesso(int idAcesso, int idPermissao);
        bool CadastrarAcessoGrupo(int idAcesso, int idGrupo);
        bool CadastrarAcessoUsuario(int idAcesso, int idUsuario);
    }
}