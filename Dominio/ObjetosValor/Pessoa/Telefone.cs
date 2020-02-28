using System.Linq;
using System.Text.RegularExpressions;

namespace Dominio.ObjetosValor.Pessoa
{
    public class Telefone
    {
        public Telefone(string telefone)
        {
            ValorNumerico = RemoverFormatacao(telefone);
            ValorFormatado = FormatarTelefone(telefone);
        }

        public Telefone(long telefone)
        {
            ValorNumerico = telefone;
            ValorFormatado = FormatarTelefone(telefone.ToString());
        }

        public long ValorNumerico { get; set; }
        public string ValorFormatado { get; set; }

        public long RemoverFormatacao(string telefone)
        {
            var numeroTelefone = telefone.Where(c => char.IsNumber(c)).ToArray();
            return long.Parse(new string(numeroTelefone));
        }

        public string FormatarTelefone(string telefone)
        {
            string numeroTelefone = RemoverFormatacao(telefone).ToString();

            string formato = numeroTelefone.StartsWith("0") ?
                @"(\d{3})(\d{5})(\d{4})" : @"(\d{2})(\d{5})(\d{4})";

            return Regex.Replace(numeroTelefone, formato, "($1) $2-$3");
        }

        public byte ObterDDD()
        {
            var ddd = ValorNumerico.ToString().Take(2).ToString();
            return byte.Parse(ddd);
        }

        public long ObterNumero()
        {
            var numero = ValorNumerico.ToString().Substring(4).ToString();
            return byte.Parse(numero);
        }
    }
}