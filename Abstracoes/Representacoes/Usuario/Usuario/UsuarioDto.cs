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
        public UsuarioDto(int id, string usuario, Senha senha, DateTime dataCriacao, bool ativo, GrupoDto grupo, PessoaDto pessoa)
        {
            Id = id;
            Usuario = usuario;
            Senha = senha;
            DataCriacao = dataCriacao;
            Ativo = ativo;
            Grupo = grupo;
            Pessoa = pessoa;
        }
        public int Id { get; set; }
        public string Usuario { get; set; }
        public Senha Senha { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
        public GrupoDto Grupo { get; set; }
        public PessoaDto Pessoa { get; set; }
    }
}
