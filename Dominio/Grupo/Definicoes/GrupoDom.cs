﻿using System.Linq;

namespace Dominio.Grupo.Definicoes
{
    public class GrupoDom
    {
        public GrupoDom(string nome, int nivel) {
            Nome = nome;
            Nivel = nivel;
        }

        public string Nome { get; }
        public int Nivel { get; }

        public bool NomeValido() => Nome.Length > 5 && !Nome.Any(x => char.IsNumber(x));
    }
}