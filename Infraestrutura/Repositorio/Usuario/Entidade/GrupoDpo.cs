using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Infraestrutura.Repositorio.Usuario.Entidade
{
    public class GrupoDpo
    {
        public GrupoDpo()
        {
        }

        public GrupoDpo(string nome, string descricao, int? pai)
        {
            Id = 0;
            Nome = nome;
            Descricao = descricao;
            Pai = pai.HasValue ? pai.Value : 0;
        }

        public GrupoDpo(int id, string nome, string descricao, int pai)
        {
            this.Id = id;
            this.Nome = nome;
            this.Descricao = descricao;
            this.Pai = pai;
        }

        [Description("ID_GRUPO")]
        public int Id { get; set; }
        [Description("NOME")]
        [StringLength(80)]
        public string Nome { get; set; }
        [Description("DESCRICAO")]
        [StringLength(200)]
        public string Descricao { get; set; }
        [Description("ID_PAI")]
        public int Pai { get; set; }
    }
}
