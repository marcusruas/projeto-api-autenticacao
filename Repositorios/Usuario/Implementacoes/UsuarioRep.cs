using Abstracoes.Representacoes.Usuario.Usuario;
using MandradePkgs.Conexoes;
using Repositorios.Usuario.Interfaces;

namespace Repositorios.Usuario.Implementacoes
{
    public class UsuarioRep : IUsuarioRep
    {
        private IConexaoSQL _conexao { get; }

        public UsuarioRep(IConexaoSQL conexao)
        {
            _conexao = conexao;
        }

        public void InserirUsuario(UsuarioDpo usuario)
        {
            throw new System.NotImplementedException();
        }
    }
}