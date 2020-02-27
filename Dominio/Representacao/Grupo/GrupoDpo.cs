using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Representacao.Grupo
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
        [Description("NIVEL")]
        public int Nivel { get; set; }
        [Description("JUSTIFICATIVA")]
        public string Justificativa { get; set; }
    }
}
