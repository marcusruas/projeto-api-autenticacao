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
        private IMapper _mapeador;
        public UsuarioController(IUsuarioSrv servico, IMapper mapper)
        {
            _servico = servico;
            _mapeador = mapper;
        }

        [HttpGet]
        public UsuarioDto ObterUsuarioPorNome(string nome)
        {
            return _mapeador.Map<UsuarioDto>(_servico.BuscarUsuarioPorNome(nome));
        }
    }
}