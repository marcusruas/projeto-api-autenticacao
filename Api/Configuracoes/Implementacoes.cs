using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;

namespace Api.Configuracoes
{
    public class Implementacoes
    {
        private IApplicationBuilder Aplicacao { get; }
        private IWebHostEnvironment Ambiente { get; }

        public Implementacoes(IApplicationBuilder aplicacao, IWebHostEnvironment ambiente)
        {
            Aplicacao = aplicacao;
            Ambiente = ambiente;
        }

        public void ConfigurarAplicacao()
        {
            if (Ambiente.IsDevelopment())
                Aplicacao.UseDeveloperExceptionPage();

            Aplicacao.UseSwagger();
            Aplicacao.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Usuário API v1");
            });

            Aplicacao.UseCors("Permissionamentos");
            Aplicacao.UseHttpsRedirection();
            Aplicacao.UseRouting();
            Aplicacao.UseAuthorization();
            Aplicacao.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
