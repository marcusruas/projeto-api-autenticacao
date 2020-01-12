using System.ComponentModel;

namespace Dominio.Grupo.Definicoes
{
    public class GrupoDho
    {
        [Description("ID_GRUPO")]
        public int IdGrupo { get; set; }
        [Description("NOME")]
        public string Nome { get; set; }
        [Description("NIVEL")]
        public int Nivel { get; set; }
    }
}
