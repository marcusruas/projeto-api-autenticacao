using Abstracoes.Representacoes.Usuario.Grupo;
using Abstracoes.Representacoes.Usuario.Pessoa;
using Abstracoes.Representacoes.Usuario.Usuario;
using MandradePkgs.Retornos;
using Microsoft.AspNetCore.Mvc;
using Servicos.Usuario.Interfaces;

namespace Api.Controllers.Usuarios
{
    [Produces("application/json")]
    [ApiController]
    public class UsuariosController : ControllerApi
    {
        private IUsuarioSrv _servico { get; }

        public UsuariosController(IUsuarioSrv servico)
        {
            _servico = servico;
        }

        [HttpGet]
        [Route("/usuarios/{id}/pessoa")]
        public RespostaApi<PessoaDto> PesquisarPessoaUsuario(int id) =>
            RespostaPadrao(_servico.ObterPessoaUsuario(id));

        [HttpGet]
        [Route("/usuarios/{id}/grupo")]
        public RespostaApi<GrupoDto> PesquisarGrupoUsuario(int id) =>
            RespostaPadrao(_servico.ObterGrupoUsuario(id));

        [HttpPost]
        [Route("/usuarios/")]
        public RespostaApi Cadastrar(UsuarioInclusaoDto usuario) =>
            RespostaPadrao(_servico.IncluirUsuario(usuario));

        [HttpPut]
        [Route("/usuarios/{id}")]
        public RespostaApi AlterarAtividade(int id, bool ativo) =>
            RespostaPadrao(_servico.AtualizarAtividadeUsuario(id, ativo));

        [HttpPut]
        [Route("/usuarios/{id}/alterar-senha")]
        public RespostaApi AlterarSenha(int id, UsuarioAlteracaoSenhaDto alteracao) =>
            RespostaPadrao(_servico.AtualizarSenhaUsuario(id, alteracao));

        [HttpDelete]
        [Route("/usuarios/{id}")]
        public RespostaApi Excluir(int id) =>
            RespostaPadrao(_servico.ExcluirUsuario(id));

    }
}