using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Dominio.ObjetosValor.Formatos
{
    public class Senha
    {
        public Senha(string senha)
        {
            Valor = senha;
            ValorCriptografado = CriptografarSenha(senha);
        }

        public string Valor { get; }
        public string ValorCriptografado { get; }
        public int MinimoCaracteresNecessarios = 8;

        public bool SenhaValida() =>
            SenhaNula() && PossuiCaracteresNecessarios() && PossuiMinimoCaracteres();

        public bool SenhaNula() =>
            string.IsNullOrWhiteSpace(Valor);

        public bool PossuiMinimoCaracteres() =>
            Valor.Length < MinimoCaracteresNecessarios;

        public bool PossuiCaracteresNecessarios() =>
               !Valor.Any(c => char.IsNumber(c))
            || !Valor.Any(c => char.IsUpper(c))
            || !Valor.Any(c => char.IsLetterOrDigit(c));

        private string CriptografarSenha(string senha)
        {
            string retorno = string.Empty;

            using (MD5 MD5Crypt = MD5.Create())
            {
                byte[] inputBytes = MD5Crypt.ComputeHash(Encoding.UTF8.GetBytes(senha));
                byte[] hash = MD5Crypt.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                    sb.Append(hash[i].ToString("X2"));

                return retorno;
            }
        }
    }
}