using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Comunicacao.Conexoes.ConexaoSQL.Interface;
using Comunicacao.Conexoes.ConexaoSQL.Implementacao;

namespace IoC
{
    public class IoC
    {
        public static void ConfigurarCamadaComunicacao(IServiceCollection servicos)
        {
            servicos.AddScoped<IConexaoSQL, ConexaoSQL>();
        }
        public static void ConfigurarCamadaRepositorio(IServiceCollection servicos)
        {
        }
        public static void ConfigurarCamadaServico(IServiceCollection servicos)
        {
        }
    }
}
