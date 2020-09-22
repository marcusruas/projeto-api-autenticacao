using System;
using Abstracoes.Representacoes.Usuario.Usuario;
using Dominio.Logica.Usuario;
using MandradePkgs.Mensagens;

namespace Abstracoes.Builders.Usuario
{
    public class UsuarioBuilder
    {
        private UsuarioDom usuario { get; set; }

        public UsuarioBuilder ConstruirObjeto(UsuarioInclusaoDto usuario, GrupoDom grupo, PessoaDom pessoa)
        {
            this.usuario = new UsuarioDom(
                0,
                usuario.Usuario,
                usuario.Senha,
                grupo,
                pessoa
            );

            return this;
        }

        public UsuarioBuilder AdicionarMensageria(IMensagensApi mensagens)
        {
            if (usuario != null)
                usuario.DefinirMensagens(mensagens);
            else
                throw new Exception("Grupo ainda não construido");

            return this;
        }

        public UsuarioDom Construir() =>
            this.usuario;
    }
}