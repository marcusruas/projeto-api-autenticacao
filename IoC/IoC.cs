using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Comunicacao.ConexaoBanco.Interface;
using Comunicacao.ConexaoBanco.Implementacao;
using Repositorio.Usuario.Implementacao;
using Repositorio.Usuario.Interface;

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
            servicos.AddScoped<IUsuarioRep, UsuarioRep>();
        }
        public static void ConfigurarCamadaServico(IServiceCollection servicos)
        {

        }
    }
}
