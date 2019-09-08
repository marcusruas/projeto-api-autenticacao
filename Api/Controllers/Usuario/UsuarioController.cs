using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacao;
using Aplicacao.Usuario;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servico.Usuario.Interface;

namespace Api.Controllers.Usuario
{
    [Route("Usuario/[action]")]
    [ApiController]
    public class UsuarioController
    {
        private IUsuarioSrv _servico;
        public UsuarioController(IUsuarioSrv servico)
        {
            _servico = servico;
        }

        [HttpGet]
        public UsuarioDto ObterUsuarioPorNome(string nome)
        {
            var map = new Mapper(Mapeamento.PrepararMapeamentoDtoDominio());
            return map.Map<UsuarioDto>(_servico.BuscarUsuarioPorNome(nome));
        }
    }
}