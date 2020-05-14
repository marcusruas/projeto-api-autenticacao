using MandradePkgs.Retornos;
using Microsoft.AspNetCore.Mvc;
using Abstracoes.Representacoes.Usuario.Grupo;
using Servicos.Usuario.Interfaces;

namespace Api.Controllers.Usuarios
{
    [Route("Usuarios/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class GruposController : ControllerApi
    {
        private IGrupoSrv _grupoServico { get; }

        public GruposController(IGrupoSrv grupoServico)
        {
            _grupoServico = grupoServico;
        }

        [HttpGet]
        public RespostaApi<GrupoDto> ObterDadosGrupo(int Id) =>
            RespostaPadrao(_grupoServico.PesquisarGrupoPorId(Id));


        [HttpPost]
        public RespostaApi IncluirNovoGrupo(GrupoDto grupo) =>
            RespostaPadrao(_grupoServico.InserirNovoUsuario(grupo));

        [HttpDelete]
        public RespostaApi ExcluirGrupo(int Id) =>
            RespostaPadrao(_grupoServico.ExcluirGrupo(Id));

    }
}