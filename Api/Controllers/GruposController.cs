using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MandradePkgs.Retornos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servico.Grupo.Interface;

namespace Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class GruposController : ControllerApi
    {
        private IGrupoSrv _servico { get; }
        private IMapper _mapper { get; }

        public GruposController(IGrupoSrv servico, IMapper mapper) {
            _servico = servico;
            _mapper = mapper;
        }

        [HttpPost]
        public RespostaApi IncluirNovoGrupo(string nome, int nivel) {
            return RespostaPadrao(_servico.InserirNovoUsuario(nome, nivel));
        }
    }
}