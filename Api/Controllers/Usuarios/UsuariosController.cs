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
        public RespostaApi CadastrarUsuario(UsuarioInclusaoDto usuario) =>
            RespostaPadrao(_servico.IncluirUsuario(usuario));

        [HttpGet]
        public List<PessoaDto> PesquisarPessoasUsuario(int Id) => throw new NotImplementedException();

        [HttpGet]
        public List<GrupoDto> PesquisarGruposUsuario(int Id) => throw new NotImplementedException();

        [HttpGet]
        public List<UsuarioDto> BuscarUsuario(string Usuario, string Senha) => throw new NotImplementedException();

        [HttpPut]
        public bool AlterarAtividadeUsuario(int Id, bool Ativo) => throw new NotImplementedException();

        [HttpPut]
        public bool AlterarSenhaUsuario(UsuarioAlteracaoSenhaDto Usuario) => throw new NotImplementedException();

        [HttpDelete]
        public bool DeletarUsuario(int Id) => throw new NotImplementedException();

    }
}