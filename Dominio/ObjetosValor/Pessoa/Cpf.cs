using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dominio.ObjetosValor.Pessoa
{
    public class Cpf
    {
        public Cpf(string cpf)
        {
            this.ValorFormatado = FormatarCpf(cpf);
            this.ValorNumerico = RemoverFormatacao(cpf);
        }

        public string ValorFormatado { get; set; }
        public string ValorNumerico { get; set; }

        public string RemoverFormatacao(string cpf)
        {
            var numeroCpf = cpf.Where(c => char.IsNumber(c)).ToArray();
            return new string(numeroCpf);
        }

        public string FormatarCpf(string cpf)
        {
            var numeroCpf = RemoverFormatacao(cpf);
            string formato = @"(\d{3})(\d{3})(\d{3})(\d{2})";
            return Regex.Replace(numeroCpf, formato, "$1.$2.$3-$4");
        }

        public bool CpfValido()
        {
            if (ValorNumerico.Length != 11)
                return false;

            return DigitosFinaisValidos();
        }

        private bool DigitosFinaisValidos()
        {
            int primeiroDigito = int.Parse(ValorNumerico[9].ToString());
            int segundoDigito = int.Parse(ValorNumerico[10].ToString());
            int somaDigitos = 0;
            bool primeiroDigitoValido = false;
            bool segundoDigitoValido = false;
            int restoDivisao = 0;

            for (int i = 1; i < (ValorNumerico.Length - 2); i++)
            {
                int digito = int.Parse(ValorNumerico[i - 1].ToString());
                somaDigitos += digito * (11 - i);
            }
            restoDivisao = (somaDigitos * 10) % 11;
            restoDivisao = restoDivisao == 10 ? 0 : restoDivisao;
            primeiroDigitoValido = primeiroDigito == restoDivisao;

            somaDigitos = 0;

            for (int i = 1; i < ValorNumerico.Length; i++)
            {
                int digito = int.Parse(ValorNumerico[i - 1].ToString());
                somaDigitos += digito * (12 - i);
            }
            restoDivisao = (somaDigitos * 10) % 11;
            restoDivisao = restoDivisao == 10 ? 0 : restoDivisao;
            segundoDigitoValido = segundoDigito == restoDivisao;

            return primeiroDigitoValido && segundoDigitoValido;
        }
    }
}