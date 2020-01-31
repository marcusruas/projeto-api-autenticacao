﻿using MandradePkgs.Mensagens;
using System;
using System.Linq;

namespace Dominio.Grupo
{
    public class GrupoDom {
        public GrupoDom(string nome, string descricao, NivelGrupo nivel, string justificativa, IMensagensApi mensagens) {
            Nome = nome;
            Descricao = descricao;
            Nivel = nivel;
            Justificativa = justificativa;
            _mensagens = mensagens;
        }

        public GrupoDom(string nome, NivelGrupo nivel, string justificativa, IMensagensApi mensagens) {
            Nome = nome;
            Nivel = nivel;
            Justificativa = justificativa;
            _mensagens = mensagens;
        }

        public GrupoDom(string nome, string descricao, NivelGrupo nivel, IMensagensApi mensagens) {
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

        public void ValidarNome() {
            if (Nome.Length <= 5)
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, 
                                             "Nome do grupo deve ter mais de 5 caractéres");
            if (Nome.Any(x => char.IsNumber(x)))
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, 
                                             "Nome do grupo não pode conter números");
        }

        public void ValidarDescricao(){
            if(!string.IsNullOrEmpty(Descricao))
                if(Descricao.Length <= 15)
                    _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, 
                                                 "Descrição do grupo deve conter mais de 15 caractéres");
        }

        public void ValidarJustificativa(){
            if(!string.IsNullOrEmpty(Justificativa)) {
                if(Justificativa.Length <= 15)
                    _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, 
                                                 "Justificativa para nível devem conter mais de 15 caractéres");
            }    
        }

        public void ValidarJustificativaParaNivel() {
            if (Nivel > NivelGrupo.Gerente && !string.IsNullOrEmpty(Justificativa))
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, 
                                             "Grupos com nível inferior a Gerente não necessitam de justificativa");

            if (Nivel < NivelGrupo.Gerente && string.IsNullOrEmpty(Justificativa))
                _mensagens.AdicionarMensagem(TipoMensagem.FalhaValidacao, 
                                             "Grupos com nivel acima de Gerente devem possuir justificativa");
        }

    }
}