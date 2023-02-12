using System.Text;

namespace Loxifi.Extensions
{
    internal static class StringExtensions
    {
        public static string FormEncode(this string toEncode)
        {
            if (string.IsNullOrEmpty(toEncode))
            {
                return string.Empty;
            }

            StringBuilder sb = new();

            for (int i = 0; i < toEncode.Length; i++)
            {
                char c = toEncode[i];

                _ = c == ' ' ? sb.Append('+') : sb.Append(Uri.EscapeDataString(c.ToString()));
            }

            return sb.ToString();
        }
    }
}