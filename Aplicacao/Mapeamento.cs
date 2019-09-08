using Aplicacao.Usuario;
using AutoMapper;
using Dominio.Usuario.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacao
{
    public class Mapeamento
    {
        public static MapperConfiguration PrepararMapeamentoDtoDominio()
        {
            return new MapperConfiguration(cfg => {
                cfg.CreateMap<IUsuario, UsuarioDto>();
            });
        }
    }
}
