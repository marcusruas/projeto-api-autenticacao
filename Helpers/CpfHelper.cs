namespace Helpers
{
    public class CpfHelper
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

        public static bool ValidarCpf(string cpf) {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
    }
}