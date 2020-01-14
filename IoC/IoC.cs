﻿using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Repositorio.Grupo.Interface;
using Repositorio.Grupo.Implementacao;

namespace IoC
{
    public class IoC
    {
        public static void ConfigurarCamadaRepositorio(IServiceCollection servicos)
        {
            servicos.AddScoped<IGrupoRep, GrupoRep>();
        }

        public static void ConfigurarCamadaServico(IServiceCollection servicos)
        {
        }
    }
}
