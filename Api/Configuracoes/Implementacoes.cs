using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Configuracoes
{
    public class Implementacoes
    {
        private IApplicationBuilder Aplicacao { get; }
        private IHostingEnvironment Ambiente { get; }

        public Implementacoes(IApplicationBuilder aplicacao, IHostingEnvironment ambiente)
        {
            Aplicacao = aplicacao;
            Ambiente = ambiente;
        }

        public void ConfigurarAplicacao()
        {
            if (Ambiente.IsDevelopment())
            {
                EmDesenvolvimento();
            }
            else
            {
                EmProducao();
            }
        }

        private void EmDesenvolvimento()
        {
            Aplicacao.UseDeveloperExceptionPage();
            Aplicacao.UseSwagger();
            Aplicacao.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Usuário API v1");
            });

            Aplicacao.UseCors("Permissionamentos");
            Aplicacao.UseHttpsRedirection();

            Aplicacao.UseMvc();
        }

        private void EmProducao()
        {
            Aplicacao.UseHsts();
            Aplicacao.UseSwagger();
            Aplicacao.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Usuário API v1");
            });

            Aplicacao.UseCors("Permissionamentos");
            Aplicacao.UseHttpsRedirection();

            Aplicacao.UseMvc();
        }
    }
}
