using System.Text.RegularExpressions;

namespace Helpers
{
    public class CpfFormat
    {
        public static string FormatarCpf(string cpf) {
            var cpfFormatado = string.Empty;

            for (int i = 0; i < cpf.Length; i++) {
                cpfFormatado += cpf[i];

                if (i == 2 || i == 5)
                    cpfFormatado += '.';

                if (i == 8)
                    cpfFormatado += '-';
            }

            return cpfFormatado;
        }

        public static long RemoverFormatacao(string cpf) {
            return long.Parse(cpf.Replace(".", string.Empty)
                                 .Replace("-", string.Empty));
        }
    }
}
