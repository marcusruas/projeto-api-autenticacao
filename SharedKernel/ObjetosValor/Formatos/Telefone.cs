using System.Linq;
using System.Text.RegularExpressions;

namespace SharedKernel.ObjetosValor.Formatos
{
    public class Telefone
    {
        public Telefone(string ddd, string numero)
        {
            Ddd = new string(ddd.Where(c => char.IsNumber(c)).ToArray());
            Numero = new string(numero.Where(c => char.IsNumber(c)).ToArray());

            string TelefoneConcatenado = Ddd + Numero;

            ValorNumerico = RemoverFormatacao(TelefoneConcatenado);
            ValorFormatado = FormatarTelefone(TelefoneConcatenado);
        }

        public string Ddd { get; }
        public string Numero { get; }
        public long ValorNumerico;
        public string ValorFormatado;

        public long RemoverFormatacao(string telefone)
        {
            var numeroTelefone = telefone.Where(c => char.IsNumber(c)).ToArray();
            return long.Parse(new string(numeroTelefone));
        }

        public string FormatarTelefone(string telefone)
        {
            string numeroTelefone = RemoverFormatacao(telefone).ToString();
            string formato = @"(\d{2})(\d{5})(\d{4})";

            return Regex.Replace(numeroTelefone, formato, "($1) $2-$3");
        }

        public long ObterValorNumerico() => ValorNumerico;
        public string ObterValorFormatado() => ValorFormatado;
    }
}