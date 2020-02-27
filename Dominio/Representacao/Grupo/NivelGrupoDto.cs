namespace Dominio.Representacao.Grupo
{
    public class NivelGrupoDto
    {
        public NivelGrupoDto()
        {
        }


        public NivelGrupoDto(int nivel, string descricao)
        {
            this.Nivel = nivel;
            this.Descricao = descricao;
        }

        public int Nivel { get; set; }
        public string Descricao { get; set; }
    }
}
