using Aplicacao.Representacoes.Autenticacao;
using MandradePkgs.Conexoes.Configuracao;
using MandradePkgs.Mensagens.Configuracao;
using MandradePkgs.Retornos.Configuracao;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
            ServicosStartup.AddMvc(ConfigurarOpcoesMvc()).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            ConfigurarPacotesApi(ServicosStartup, Startup);
            AdicionarMiddlewares(ServicosStartup);
            ConfigurarTokens();
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
            servicos.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Usuário API", Version = "v1" });
            });

            servicos.AddCors(options =>
            {
                options.AddPolicy("Permissionamentos",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader()
                                      .AllowAnyOrigin());
            });
        }

        private void ConfigurarTokens()
        {
            var assinatura = new AssinaturaTokenDto();

            var configuracoes = new ConfiguracoesTokenDto();
            new ConfigureFromConfigurationOptions<ConfiguracoesTokenDto>(
                Configuracao.GetSection("ConfiguracoesToken")
            ).Configure(configuracoes);

            ServicosStartup.AddSingleton(assinatura);
            ServicosStartup.AddSingleton(configuracoes);
            ServicosStartup.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var parametrosValidacao = bearerOptions.TokenValidationParameters;
                parametrosValidacao.IssuerSigningKey = assinatura.Key;
                parametrosValidacao.ValidAudience = configuracoes.Audience;
                parametrosValidacao.ValidIssuer = configuracoes.Originador;
                parametrosValidacao.ClockSkew = TimeSpan.Zero;

                // Valida a assinatura de um token recebido
                parametrosValidacao.ValidateIssuerSigningKey = true;
                // Verifica se um token recebido ainda é válido
                parametrosValidacao.ValidateLifetime = true;
            });

            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos deste projeto
            ServicosStartup.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
        }
    }
}
