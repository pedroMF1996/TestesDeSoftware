using System.Text.RegularExpressions;

namespace NerdStore.BDD.Tests.Config
{
    public static class TestExtensions
    {
        public static int ApenasNumeros(this string value)
        {
            return int.Parse(value.Where(c => char.IsDigit(c)).ToArray());
        }

        public static string LimparValor(this string valor)
        {
            return valor.Replace('$', ' ').Trim();
        }
    }
}
