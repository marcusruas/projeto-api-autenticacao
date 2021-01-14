using MandradePkgs.Conexoes.Configuracao;
using MandradePkgs.Mensagens.Configuracao;
using MandradePkgs.Retornos.Configuracao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace Api.Configuracoes
{
    public class Middlewares
    {
        public Middlewares(IServiceCollection servicosStartup, IConfiguration configuracao, Type startup)
        {
            ServicosStartup = servicosStartup;
            Configuracao = configuracao;
            Startup = startup;
        }

        private IServiceCollection ServicosStartup { get; }
        private IConfiguration Configuracao { get; }
        private Type Startup { get; }

        public void ConfigurarServicos()
        {
            ServicosStartup.AddMvc(ConfigurarOpcoesMvc());

            ConfigurarPacotesApi(ServicosStartup, Startup);
            AdicionarMiddlewares(ServicosStartup);
        }

        private void ConfigurarPacotesApi(IServiceCollection servicos, Type startup)
        {
            servicos.ImplementarConexaoSQL(startup);
            servicos.ImplementarMensagensServico();
        }

        private Action<MvcOptions> ConfigurarOpcoesMvc() => 
            cfg => cfg.ImplementarFiltrosRetorno();

        private void AdicionarMiddlewares(IServiceCollection servicos)
        {
            string versao = "v1";
            OpenApiInfo configsSwagger = new OpenApiInfo { Title = "Usuario API", Version = versao };
            servicos.AddSwaggerGen(c => { c.SwaggerDoc(versao, configsSwagger); });

            servicos.AddCors(options =>
            {
                options.AddPolicy("Permissionamentos",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader()
                                      .AllowAnyOrigin());
            });
        }
    }
}
