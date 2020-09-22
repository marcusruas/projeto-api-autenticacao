using System;
using Abstracoes.Representacoes.Usuario.Grupo;
using Dominio.Logica.Usuario;
using MandradePkgs.Mensagens;

namespace Abstracoes.Builders.Usuario
{
    public class GrupoBuilder
    {
        private GrupoDom grupo { get; set; }

        public GrupoBuilder ConstruirObjeto(GrupoInclusaoDto grupo)
        {
            this.grupo = new GrupoDom(
                grupo.Pai.HasValue ? grupo.Pai.Value : 0,
                grupo.Nome,
                grupo.Descricao
            );

            return this;
        }

        public GrupoBuilder ConstruirObjeto(GrupoDto grupo)
        {
            this.grupo = new GrupoDom(
                grupo.Id,
                grupo.Nome,
                grupo.Descricao,
                grupo.Pai
            );

            return this;
        }

        public GrupoBuilder AdicionarMensageria(IMensagensApi mensagens)
        {
            if (grupo != null)
                grupo.DefinirMensagens(mensagens);
            else
                throw new Exception("Grupo ainda não construido");

            return this;
        }

        public GrupoDom Construir() =>
            this.grupo;
    }
}