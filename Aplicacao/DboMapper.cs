using Aplicacao.Grupo;
using AutoMapper;
using Dapper.FluentMap.Mapping;
using System;
using System.ComponentModel;
using System.Reflection;

namespace Aplicacao
{
    public class DboMapper<T> : EntityMap<T> where T : class
    {
        public DboMapper() {
            CriarMapeamento();
        }

        private void CriarMapeamento() {
            Type classe = typeof(T);
            foreach (var prop in classe.GetProperties()) {
                var descricao = ObterDescription(prop);
                Map(p => prop)
                .ToColumn(descricao);
            }
        }

        private string ObterDescription(PropertyInfo prop) {
            var description = prop.GetCustomAttribute<DescriptionAttribute>();
            return description == null ? prop.Name : description.Description;
        }
    }
}
