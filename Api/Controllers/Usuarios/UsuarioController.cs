using System;
using System.Collections.Generic;
using Abstracoes.Representacoes.Usuario.Grupo;
using Abstracoes.Representacoes.Usuario.Pessoa;
using Abstracoes.Representacoes.Usuario.Usuario;
using Aplicacao.Representacoes.Usuario;
using AutoMapper;
using MandradePkgs.Retornos;
using Microsoft.AspNetCore.Mvc;
using Servicos.Usuario.Interfaces;

namespace Api.Controllers.Usuarios
{
    [Route("[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class UsuariosController : ControllerApi
    {
        private IUsuarioSrv _servico { get; }

        public UsuariosController(IUsuarioSrv servico)
        {
            _servico = servico;
        }

        [HttpPost]
        public RespostaApi<TokenDto> Autenticar(
            string Usuario,
            string Senha,
            [FromServices] ConfiguracoesTokenDto configsToken,
            [FromServices] AssinaturaTokenDto assinatura
        ) =>
            RespostaPadrao(_servico.Autenticar(Usuario, Senha, configsToken, assinatura));

        [HttpPost]
        public RespostaApi Cadastrar(UsuarioInclusaoDto usuario) =>
            RespostaPadrao(_servico.IncluirUsuario(usuario));

        [HttpGet]
        public RespostaApi<PessoaDto> Pesquisar(int Id) =>
            RespostaPadrao(_servico.ObterPessoaUsuario(Id));

        [HttpGet]
        public RespostaApi<GrupoDto> PesquisarGrupo(int Id) =>
            RespostaPadrao(_servico.ObterGrupoUsuario(Id));

        [HttpPut]
        public RespostaApi AlterarAtividade(int Id, bool Ativo) =>
            RespostaPadrao(_servico.AtualizarAtividadeUsuario(Id, Ativo));

        [HttpPut]
        public RespostaApi AlterarSenha(UsuarioAlteracaoSenhaDto Usuario) =>
            RespostaPadrao(_servico.AtualizarSenhaUsuario(Usuario));

        [HttpDelete]
        public RespostaApi Excluir(int Id) =>
            RespostaPadrao(_servico.ExcluirUsuario(Id));

    }
}