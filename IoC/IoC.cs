using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Repositorio.Grupo.Interface;
using Repositorio.Grupo.Implementacao;
using Servico.Grupo.Interface;
using Servico.Grupo.Implementacao;
using Repositorio.Pessoa.Interface;
using Repositorio.Pessoa.Implementacao;
using Servico.Pessoa.Interface;
using Servico.Pessoa.Implementacao;

namespace IoC
{
    public class IoC
    {
        public static void ConfigurarCamadaRepositorio(IServiceCollection servicos)
        {
            servicos.AddScoped<IGrupoRep, GrupoRep>();
            servicos.AddScoped<IPessoaRep, PessoaRep>();
        }

        public static void ConfigurarCamadaServico(IServiceCollection servicos)
        {
            servicos.AddScoped<IGrupoSrv, GrupoSrv>();
            servicos.AddScoped<IPessoaSrv, PessoaSrv>();
        }
    }
}
