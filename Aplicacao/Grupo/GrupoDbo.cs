using System.ComponentModel;

namespace Aplicacao.Grupo
{
    public class GrupoDbo
    {
        [Description("ID_GRUPO")]
        public int IdGrupo { get; set; }
        [Description("NOME")]
        public string Nome { get; set; }
        [Description("NIVEL")]
        public int Nivel { get; set; }
    }
}
