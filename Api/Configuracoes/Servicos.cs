using Aplicacao;
using AutoMapper;
using MandradePkgs.Conexoes.Configuracao;
using MandradePkgs.Retornos.Configuracao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            ConfigurarMapeamentos(ServicosStartup);
            ConfigurarMapeamentos(ServicosStartup);
            ConfigurarPacotesApi(ServicosStartup, Startup);
            ConfigurarInjecoesDependencia(ServicosStartup);
            AdicionarMiddlewares(ServicosStartup);
        }

        private void ConfigurarInjecoesDependencia(IServiceCollection servicos) {
            IoC.IoC.ConfigurarCamadaRepositorio(servicos);
            IoC.IoC.ConfigurarCamadaServico(servicos);
        }

        private void ConfigurarMapeamentos(IServiceCollection servicos) {
            var mapeamentoDominio = Mapeamento.PrepararMapeamentoDtoDominio();
            mapeamentoDominio.CompileMappings();
            mapeamentoDominio.AssertConfigurationIsValid();

            servicos.AddScoped<IMapper>(x => new Mapper(mapeamentoDominio));
        }

        private void ConfigurarPacotesApi(IServiceCollection servicos, Type startup) {
            servicos.ImplementarConexaoSQL(startup);
            servicos.ImplementarMensagensRetorno();
        }

        private Action<MvcOptions> ConfigurarOpcoesMvc() {
            return cfg => cfg.ImplementarFiltrosRetorno();
        }

        private void AdicionarMiddlewares(IServiceCollection servicos) {
            servicos.AddSwaggerGen(c => {
                c.SwaggerDoc("v2", new Info { Title = "PadraoAPI", Version = "v2" });
            });

            servicos.AddCors(options => {
                options.AddPolicy("PermissionamentoReact",
                    builder => builder.WithOrigins("http://localhost:3000")
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .AllowAnyOrigin());
            });
        }
    }
}
