using SharedKernel.ObjetosValor.Enum;

namespace Abstracoes.Representacoes.Usuario.Grupo
{
    public class GrupoAtualizacaoDto
    {
        public int Id { get; set; }
        public NivelGrupo Nivel { get; set; }
        public string Justificativa { get; set; }
    }
}