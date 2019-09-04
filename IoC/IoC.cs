using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Comunicacao.ConexaoBanco.Interface;
using Comunicacao.ConexaoBanco.Implementacao;

namespace IoC
{
    public class IoC
    {
        public static void ConfigurarCamadaComunicacao(IServiceCollection servicos)
        {
            servicos.AddScoped<IConexaoBanco, ConexaoBanco>();
        }
        public static void ConfigurarCamadaRepositorio(IServiceCollection servicos)
        {

        }
        public static void ConfigurarCamadaServico(IServiceCollection servicos)
        {

        }
    }
}
