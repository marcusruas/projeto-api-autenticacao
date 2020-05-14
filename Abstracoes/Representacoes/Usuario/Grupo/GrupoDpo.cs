﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Abstracoes.Representacoes.Usuario.Grupo
{
    public class GrupoDpo
    {
        [Description("ID_GRUPO")]
        public int Id { get; set; }
        [Description("NOME")]
        [StringLength(80)]
        public string Nome { get; set; }
        [Description("DESCRICAO")]
        [StringLength(200)]
        public string Descricao { get; set; }
    }
}
