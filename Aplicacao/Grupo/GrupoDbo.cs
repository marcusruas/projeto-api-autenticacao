﻿using Dominio.Grupo;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Aplicacao.Grupo
{
    public class GrupoDbo
    {
        [Description("ID_GRUPO")]
        public int IdGrupo { get; set; }
        [Description("NOME")]
        [StringLength(80)]
        public string Nome { get; set; }
        [Description("DESCRICAO")]
        [StringLength(200)]
        public string Descricao { get; set; }
        [Description("NIVEL")]
        public int Nivel { get; set; }
    }
}