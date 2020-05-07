using System;
using System.Linq;
using SharedKernel.ObjetosValor.Formatos;
using MandradePkgs.Mensagens;
using Abstracoes.Representacoes.Usuario.Grupo;
using Abstracoes.Representacoes.Usuario.Pessoa;

namespace Abstracoes.Representacoes.Usuario.Usuario
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public Senha Senha { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
        public GrupoDto Grupo { get; set; }
        public PessoaDto Pessoa { get; set; }
    }
}
