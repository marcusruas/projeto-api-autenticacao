﻿using Aplicacao;
using AutoMapper;
using Dapper.FluentMap;
using MandradePkgs.Conexoes.Configuracao;
using MandradePkgs.Mensagens.Configuracao;
using MandradePkgs.Retornos.Configuracao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace Api.Configuracoes
{
    public class Servicos
    {
        public Servicos(IServiceCollection servicosStartup, Type startup) {
            ServicosStartup = servicosStartup;
            Startup = startup;
        }

        private IServiceCollection ServicosStartup { get; }
        private Type Startup { get; }

        public void ConfigurarServicos() {
            ServicosStartup.AddMvc(ConfigurarOpcoesMvc()).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            ConfigurarMapeamentos();
            ConfigurarPacotesApi(ServicosStartup, Startup);
            ConfigurarInjecoesDependencia(ServicosStartup);
            AdicionarMiddlewares(ServicosStartup);
        }

        private void ConfigurarInjecoesDependencia(IServiceCollection servicos) {
            IoC.IoC.ConfigurarCamadaRepositorio(servicos);
            IoC.IoC.ConfigurarCamadaServico(servicos);
        }

        private void ConfigurarMapeamentos() {
            FluentMapper.Initialize(configuracoes =>
            {
                configuracoes.MapearObjetosBanco();
            });
        }

        private void ConfigurarPacotesApi(IServiceCollection servicos, Type startup) {
            servicos.ImplementarConexaoSQL(startup);
            servicos.ImplementarMensagensServico();
        }

        private Action<MvcOptions> ConfigurarOpcoesMvc() {
            return cfg => cfg.ImplementarFiltrosRetorno();
        }

        private void AdicionarMiddlewares(IServiceCollection servicos) {
            servicos.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info { Title = "Autenticacao API", Version = "v1" });
            });

            servicos.AddCors(options => {
                options.AddPolicy("Permissionamentos",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader()
                                      .AllowAnyOrigin());
            });
        }
    }
}
