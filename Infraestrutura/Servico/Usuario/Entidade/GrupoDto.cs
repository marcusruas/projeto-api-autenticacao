using Infraestrutura.Repositorio.Usuario.Entidade;

namespace Infraestrutura.Servico.Usuario.Entidade
{
    public class GrupoDto
    {
        public GrupoDto()
        {
        }

        public GrupoDto(int id, string nome, string descricao, int pai)
        {
            this.Id = id;
            this.Nome = nome;
            this.Descricao = descricao;
            this.Pai = pai;
        }

        public GrupoDto(GrupoDpo grupo)
        {
            Id = grupo.Id;
            Nome = grupo.Nome;
            Descricao = grupo.Descricao;
            Pai = grupo.Pai;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Pai { get; set; }
    }
}
