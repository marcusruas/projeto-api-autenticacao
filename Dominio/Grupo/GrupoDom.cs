using System.Linq;

namespace Dominio.Grupo
{
    public class GrupoDom
    {
        public GrupoDom(string nome, string descricao, NivelGrupo nivel) {
            Nome = nome;
            Descricao = descricao;
            Nivel = nivel;
        }

        public GrupoDom(string nome, NivelGrupo nivel) {
            Nome = nome;
            Nivel = nivel;
        }

        public string Nome { get; }
        public string Descricao { get; }
        public NivelGrupo Nivel { get; }

        public bool NomeValido() => Nome.Length > 5 && !Nome.Any(x => char.IsNumber(x));
    }
}