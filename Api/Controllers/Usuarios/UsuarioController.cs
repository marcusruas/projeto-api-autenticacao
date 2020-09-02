using System;
using System.Collections.Generic;
using Abstracoes.Representacoes.Usuario.Grupo;
using Abstracoes.Representacoes.Usuario.Pessoa;
using Abstracoes.Representacoes.Usuario.Usuario;
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
        public RespostaApi Cadastrar(UsuarioInclusaoDto usuario) =>
            RespostaPadrao(_servico.IncluirUsuario(usuario));

        [HttpGet]
        public RespostaApi<PessoaDto> PesquisarPessoa(int Id) =>
            RespostaPadrao(_servico.ObterPessoaUsuario(Id));

        [HttpGet]
        public RespostaApi<GrupoDto> PesquisarGrupo(int Id) =>
            RespostaPadrao(_servico.ObterGrupoUsuario(Id));

        [HttpGet]
        public RespostaApi<UsuarioDto> Validar(string Usuario, string Senha) =>
            RespostaPadrao(_servico.ValidarUsuario(Usuario, Senha));

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