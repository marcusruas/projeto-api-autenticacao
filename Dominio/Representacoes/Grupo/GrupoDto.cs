using System;
using System.ComponentModel;

namespace Dominio.Representacoes.Grupo
{
    public class GrupoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public NivelGrupoDto Nivel { get; set; }
        public string Justificativa { get; set; }
    }
}
