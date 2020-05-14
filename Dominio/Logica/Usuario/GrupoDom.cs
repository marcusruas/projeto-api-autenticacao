﻿using MandradePkgs.Mensagens;
using SharedKernel.ObjetosValor.Enum;
using System.Linq;

namespace Dominio.Logica.Usuario
{
    public class GrupoDom
    {
        public GrupoDom(int id, string nome, string descricao, IMensagensApi mensagens)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            _mensagens = mensagens;
        }

        public GrupoDom(int id, string nome, IMensagensApi mensagens)
        {
            Id = id;
            Nome = nome;
            _mensagens = mensagens;
        }

        public int Id { get; }
        public string Nome { get; }
        public string Descricao { get; }
        private IMensagensApi _mensagens { get; }

        public const int LimiteCaracteresNome = 80;
        public const int LimiteCaracteresDescricao = 200;
        public const int LimiteCaracteresJustificativa = 500;

        public void ValidarDados()
        {
            ValidarNome();
            ValidarDescricao();
        }

        public void ValidarNome()
        {
            if (string.IsNullOrEmpty(Nome))
            {
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, "Nome é obrigatório");
                return;
            }

            if (Nome.Length <= 5)
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao,
                                             "Nome do grupo deve ter mais de 5 caractéres");
            if (Nome.Length > LimiteCaracteresNome)
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao,
                                             "Nome do grupo deve ter menos de 80 caractéres");
            if (Nome.Any(x => char.IsNumber(x)))
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao,
                                             "Nome do grupo não pode conter números");
        }

        public void ValidarDescricao()
        {
            if (!string.IsNullOrEmpty(Descricao))
            {
                if (Descricao.Length <= 15)
                    _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao,
                                                 "Descrição do grupo deve conter mais de 15 caractéres");
                if (Descricao.Length > LimiteCaracteresDescricao)
                    _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao,
                                                "Descrição do grupo deve conter menos de 200 caractéres");
            }
        }
    }
}