using Infraestrutura.Servico.Usuario.Entidade;
using Infraestrutura.Servico.Usuario.Interface;
using MandradePkgs.Autenticacao.Estrutura.Token;
using MandradePkgs.Retornos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Usuarios
{
    [Route("/usuarios")]
    [Produces("application/json")]
    [ApiController]
    public class UsuariosController : ControllerApi
    {
        private IUsuarioSv _servico { get; }

        public UsuariosController(IUsuarioSv servico)
        {
            _servico = servico;
        }

        [HttpPost]
        [Route("autenticar")]
        public RespostaApi<Token> Autenticar(string Usuario, string Senha, [FromServices] ConfiguracoesToken configsToken, [FromServices] AssinaturaToken assinatura) =>
            RespostaPadrao(_servico.Autenticar(Usuario, Senha, configsToken, assinatura));

        [HttpGet]
        [Route("{id}/pessoa")]
        public RespostaApi<PessoaDto> PesquisarPessoaUsuario(int id) =>
            RespostaPadrao(_servico.ObterPessoaUsuario(id));

        [HttpGet]
        [Route("{id}/grupo")]
        public RespostaApi<GrupoDto> PesquisarGrupoUsuario(int id) =>
            RespostaPadrao(_servico.ObterGrupoUsuario(id));

        [HttpPost]
        public RespostaApi Cadastrar(UsuarioInclusaoDto usuario) =>
            RespostaPadrao(_servico.IncluirUsuario(usuario));

        [HttpPut]
        [Route("{id}")]
        public RespostaApi AlterarAtividade(int id, bool ativo) =>
            RespostaPadrao(_servico.AtualizarAtividadeUsuario(id, ativo));

        [HttpPut]
        [Route("{id}/alterar-senha")]
        public RespostaApi AlterarSenha(int id, UsuarioAlteracaoSenhaDto alteracao) =>
            RespostaPadrao(_servico.AtualizarSenhaUsuario(id, alteracao));
    }
}