﻿using Dominio.Grupo;
using System.ComponentModel;

namespace Aplicacao.Grupo
{
    public class GrupoDto
    {
        public int IdGrupo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public NivelGrupo Nivel { get; set; }
    }
}
