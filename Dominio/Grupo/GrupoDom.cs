﻿using MandradePkgs.Mensagens;
using System;
using System.Linq;

namespace Dominio.Grupo
{
    public class GrupoDom
    {
        public GrupoDom(string nome, string descricao, NivelGrupo nivel, string justificativa, IMensagensApi mensagens)
        {
            Nome = nome;
            Descricao = descricao;
            Nivel = nivel;
            Justificativa = justificativa;
            _mensagens = mensagens;
        }

        public GrupoDom(string nome, NivelGrupo nivel, string justificativa, IMensagensApi mensagens)
        {
            Nome = nome;
            Nivel = nivel;
            Justificativa = justificativa;
            _mensagens = mensagens;
        }

        public GrupoDom(string nome, string descricao, NivelGrupo nivel, IMensagensApi mensagens)
        {
            Nome = nome;
            Descricao = descricao;
            Nivel = nivel;
            _mensagens = mensagens;
        }

        public string Nome { get; }
        public string Descricao { get; }
        public NivelGrupo Nivel { get; }
        public string Justificativa { get; }
        private IMensagensApi _mensagens { get; }

        public void ValidarDados()
        {
            ValidarNome();
            ValidarDescricao();
            ValidarJustificativa();
            ValidarJustificativaParaNivel();
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
            if (Nome.Length > 80)
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao,
                                             "Nome do grupo deve ter menos de 80 caractéres");
            if (Nome.Any(x => char.IsNumber(x)))
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao,
                                             "Nome do grupo não pode conter números");
        }

        public void ValidarDescricao()
        {
            if (!string.IsNullOrEmpty(Descricao))
                if (Descricao.Length <= 15)
                    _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao,
                                                 "Descrição do grupo deve conter mais de 15 caractéres");
            if (Descricao.Length > 200)
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao,
                                             "Descrição do grupo deve conter menos de 200 caractéres");
        }

        public void ValidarJustificativa()
        {
            if (!string.IsNullOrEmpty(Justificativa))
            {
                if (Justificativa.Length <= 15)
                    _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao,
                                                 "Justificativa para nível devem conter mais de 15 caractéres");

                if (Justificativa.Length > 500)
                    _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao,
                                                 "Justificativa para nível devem conter menos de 500 caractéres");
            }
        }

        public void ValidarJustificativaParaNivel()
        {
            if (Nivel > NivelGrupo.Gerente && !string.IsNullOrEmpty(Justificativa))
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao,
                                             "Grupos com nível inferior a Gerente não necessitam de justificativa");

            if (Nivel < NivelGrupo.Gerente && string.IsNullOrEmpty(Justificativa))
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao,
                                             "Grupos com nivel acima de Gerente devem possuir justificativa");
        }

    }
}