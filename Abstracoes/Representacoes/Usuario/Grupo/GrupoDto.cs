namespace Abstracoes.Representacoes.Usuario.Grupo
{
    public class GrupoDto
    {
        public GrupoDto(int id, string nome, string descricao, int pai)
        {
            this.Id = id;
            this.Nome = nome;
            this.Descricao = descricao;
            this.Pai = pai;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Pai { get; set; }
    }
}
