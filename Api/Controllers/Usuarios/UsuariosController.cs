using System.Collections.Generic;
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
        public RespostaApi CadastrarUsuario(UsuarioInclusaoDto usuario)
        {
            _servico.IncluirUsuario(usuario);
            return RespostaPadrao();
        }
    }
}