using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using ConexaoDB.Interface;
using ConexaoDB.Implementacao;

namespace IoC
{
    public class IoC
    {
        public static void ConfigurarCamadaRepositorio(IServiceCollection servicos)
        {
            servicos.AddScoped<IConexaoSQL, ConexaoSQL>();
        }
        public static void ConfigurarCamadaServico(IServiceCollection servicos)
        {
        }
    }
}
