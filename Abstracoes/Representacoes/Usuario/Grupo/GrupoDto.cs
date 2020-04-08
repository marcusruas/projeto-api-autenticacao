using SharedKernel.ObjetosValor.Enum;

namespace Abstracoes.Representacoes.Usuario.Grupo
{
    public class GrupoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public NivelGrupo Nivel { get; set; }
        public string Justificativa { get; set; }
    }
}
