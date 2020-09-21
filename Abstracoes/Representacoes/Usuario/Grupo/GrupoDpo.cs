using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Abstracoes.Representacoes.Usuario.Grupo
{
    public class GrupoDpo
    {
        public GrupoDpo()
        {
        }

        public GrupoDpo(GrupoInclusaoDto grupo)
        {
            Id = 0;
            Nome = grupo.Nome;
            Descricao = grupo.Descricao;
            Pai = grupo.Pai.HasValue ? grupo.Pai.Value : 0;
        }

        [Description("ID_GRUPO")]
        public GrupoDpo(int id, string nome, string descricao, int pai)
        {
            this.Id = id;
            this.Nome = nome;
            this.Descricao = descricao;
            this.Pai = pai;
        }

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
