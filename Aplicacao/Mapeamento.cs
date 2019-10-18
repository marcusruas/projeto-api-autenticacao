using AutoMapper;
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

                //Exemplo de mapeamento: cfg.CreateMap<UsuarioDom, UsuarioDto>().ReverseMap();

            });
        }
    }
}
